using Group2Flight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Group2Flight.Controllers
{
	public class HomeController : Controller
	{
        private Group2FlightDatabaseContext _ctx;
        public HomeController(Group2FlightDatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public ViewResult Index(FlightsViewModel model)
        {
            var filter = new Filter($"{model.ActiveFromKey}-{model.ActiveToKey}-{DateTime.TryParse(model.ActiveDepartureDate, out DateTime activeDepartureDate)}-{model.ActiveCabinType}");
            var filters = new Filter(filter.FilterString);

            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            session.SetActiveFrom(model.ActiveFromKey);
            session.SetActiveTo(model.ActiveToKey);
            session.SetActiveDepartureDate(model.ActiveDepartureDate);
            session.SetActiveCabinType(model.ActiveCabinType);

            if (session.GetSelectedCount() == 0)
            {
                var ids = cookies.GetSelectedIds();
                if (ids.Length > 0)
                {
                    var selectedFlights = _ctx.FlightSelection
                        .Include(r => r.Flight)
                        .ThenInclude(r => r.Airline)
                        .Where(r => ids.Contains(r.FlightSelectionId.ToString()))
                        .ToList();

                    session.SetSelections(selectedFlights);
                }
            }
            IQueryable<Flight> query = _ctx.Flight
                .Include(r => r.Airline)
                .OrderBy(r => r.FlightCode);

            if (filters.HasFromKey)
                query = query.Where(r => r.From.ToString() == model.ActiveFromKey);

            if (filters.HasToKey)
                query = query.Where(r => r.To.ToString() == model.ActiveToKey);
            
            if (filters.HasCabinType)
                query = query.Where(r => r.CabinType.ToString() == model.ActiveCabinType);

            if (!string.IsNullOrEmpty(model.ActiveDepartureDate) && model.ActiveDepartureDate.ToLower() != "all")
            {
                DateTime selectedDate = DateTime.Parse(model.ActiveDepartureDate);

                query = query.Where(r => r.Date.Date == selectedDate.Date);
            }
            model.CabinTypes = _ctx.Flight.Select(f => f.CabinType).Distinct().ToList();
            model.FromCities = _ctx.Flight.Select(f => f.From).Distinct().ToList();
            model.ToCities = _ctx.Flight.Select(f => f.To).Distinct().ToList();
            model.Flight = query.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddSelectedFlights(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var flightSelection = new FlightSelection
            {
                FlightId = id,
            };

            _ctx.FlightSelection.Add(flightSelection);
            _ctx.SaveChanges();

            var selections = session.GetSelections();
            selections.Add(flightSelection);
            session.SetSelections(selections);
            cookies.SetSelectedIds(selections);

            TempData["Message"] = "Flight Confirmed!.";

            return RedirectToAction("Index", new
            {
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            });
        }

        public IActionResult SelectedFlights()
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var selectedIds = cookies.GetSelectedIds();

            var flightSelections = _ctx.FlightSelection
                .Include(r => r.Flight)
                    .ThenInclude(f => f.Airline)
                .Where(r => selectedIds.Contains(r.FlightSelectionId.ToString()))
                .ToList();

            var model = new FlightsViewModel
            {
                FlightSelection = flightSelections,
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteAllSelections()
        {
            var session = new FlightSessions(HttpContext.Session);
            var selections = session.GetSelections();

            if (selections != null && selections.Any())
            {
                var ids = selections.Select(r => r.FlightSelectionId).ToList();

                var flightSelections = _ctx.FlightSelection
                    .Where(r => ids.Contains(r.FlightSelectionId))
                    .ToList();

                _ctx.FlightSelection.RemoveRange(flightSelections);
                _ctx.SaveChanges();

                session.RemoveSelections();

                var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
                cookies.RemoveSelectedIds();
            }

            TempData["Message"] = "All selected flights cancelled successfully!";
            return RedirectToAction("SelectedFlights");
        }


        [HttpPost]
        public IActionResult CancelSelectedFlights(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var flightSelection = _ctx.FlightSelection.Find(id);
            if (flightSelection != null)
            {
                _ctx.FlightSelection.Remove(flightSelection);
                _ctx.SaveChanges();
            }

            var selections = session.GetSelections();
            var flight = selections.FirstOrDefault(r => r.FlightSelectionId == id);
            if (flight != null)
            {
                selections.Remove(flight);
                session.SetSelections(selections);
            }

            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            cookies.RemoveSelectedId(id);

            TempData["Message"] = "Selected Flight cancelled successfully!";
            return RedirectToAction("SelectedFlights");
        }


        public IActionResult Details(int id)
        {
            var flight = _ctx.Flight
                .Include(r => r.Airline)
                .FirstOrDefault(r => r.FlightId == id);
            if (flight == null)
                return NotFound();

            var session = new FlightSessions(HttpContext.Session);

            var flightsViewModel = new FlightsViewModel
            {
                Flights = flight,
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            };

            return View(flightsViewModel);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
