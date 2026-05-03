using Group2Flight.Models;
using Group2Flight.Models.DataLayer;
using Group2Flight.Models.DomainModels;
using Group2Flight.Models.ViewModels;
using Group2Flight.Models.ExtensionMethods;
using Group2Flight.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Group2Flight.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Flight> flightRepo;
        private readonly IRepository<FlightSelection> selectionRepo;

        public HomeController(
            IRepository<Flight> flightRepo,
            IRepository<FlightSelection> selectionRepo)
        {
            this.flightRepo = flightRepo;
            this.selectionRepo = selectionRepo;
        }

        public ViewResult Index(FlightsViewModel model)
        {
            var filter = new Filter(
                $"{model.ActiveFromKey}-{model.ActiveToKey}-" +
                $"{DateTime.TryParse(model.ActiveDepartureDate, out DateTime activeDepartureDate)}-" +
                $"{model.ActiveCabinType}");

            var filters = new Filter(filter.FilterString);

            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            // Save active filters
            session.SetActiveFrom(model.ActiveFromKey);
            session.SetActiveTo(model.ActiveToKey);
            session.SetActiveDepartureDate(model.ActiveDepartureDate);
            session.SetActiveCabinType(model.ActiveCabinType);

            // FIXED: Always sync session count from cookies on first load
            var ids = cookies.GetSelectedIds();

            if (ids.Length > 0)
            {
                var selectedFlights = ids.Select(id => new FlightSelection
                {
                    FlightSelectionId = int.Parse(id),
                    FlightId = int.Parse(id)
                }).ToList();

                session.SetSelections(selectedFlights);
            }
            else
            {
                session.SetSelections(new List<FlightSelection>());
            }

            var options = new QueryOptions<Flight>
            {
                Includes = "Airline"
            };

            var flights = flightRepo.List(options)
                .OrderBy(f => f.FlightCode)
                .ToList();

            if (filters.HasFromKey)
                flights = flights
                    .Where(f => f.From == model.ActiveFromKey)
                    .ToList();

            if (filters.HasToKey)
                flights = flights
                    .Where(f => f.To == model.ActiveToKey)
                    .ToList();

            if (filters.HasCabinType)
                flights = flights
                    .Where(f => f.CabinType == model.ActiveCabinType)
                    .ToList();

            if (!string.IsNullOrEmpty(model.ActiveDepartureDate) &&
                model.ActiveDepartureDate.ToLower() != "all")
            {
                DateTime selectedDate =
                    DateTime.Parse(model.ActiveDepartureDate);

                flights = flights
                    .Where(f => f.Date.Date == selectedDate.Date)
                    .ToList();
            }

            model.CabinTypes = flightRepo.List(new QueryOptions<Flight>())
                .Select(f => f.CabinType)
                .Distinct()
                .ToList();

            model.FromCities = flightRepo.List(new QueryOptions<Flight>())
                .Select(f => f.From)
                .Distinct()
                .ToList();

            model.ToCities = flightRepo.List(new QueryOptions<Flight>())
                .Select(f => f.To)
                .Distinct()
                .ToList();

            model.Flight = flights;

            return View(model);
        }
        [HttpPost]
        public IActionResult ReserveAllFlights()
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var selections = session.GetSelections();

            // If session empty, restore from cookies
            if (selections == null || !selections.Any())
            {
                var ids = cookies.GetSelectedIds();

                selections = ids.Select(id => new FlightSelection
                {
                    FlightSelectionId = int.Parse(id),
                    FlightId = int.Parse(id)
                }).ToList();
            }

            if (selections == null || !selections.Any())
            {
                TempData["Message"] = "No selected flights available.";
                return RedirectToAction("SelectedFlights");
            }

            foreach (var item in selections)
            {
                bool alreadyReserved = selectionRepo
                    .List(new QueryOptions<FlightSelection>())
                    .Any(r => r.FlightId == item.FlightId);

                if (!alreadyReserved)
                {
                    selectionRepo.Insert(new FlightSelection
                    {
                        FlightId = item.FlightId
                    });
                }
            }

            selectionRepo.Save();

            // Clear session + cookies after reserve
            session.RemoveSelections();
            cookies.RemoveSelectedIds();

            TempData["Message"] = "All selected flights reserved successfully!";

            return RedirectToAction("SelectedFlights");
        }

        [HttpPost]
        public IActionResult ReserveFlight(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            bool alreadyReserved = selectionRepo.List(
                new QueryOptions<FlightSelection>())
                .Any(r => r.FlightId == id);

            if (!alreadyReserved)
            {
                selectionRepo.Insert(new FlightSelection
                {
                    FlightId = id
                });

                selectionRepo.Save();

                TempData["Message"] = "Flight reserved successfully!";
            }
            else
            {
                TempData["Message"] = "This flight is already reserved.";
            }

            var selections = session.GetSelections();

            var selected = selections.FirstOrDefault(s => s.FlightId == id);

            if (selected != null)
            {
                selections.Remove(selected);
                session.SetSelections(selections);
                cookies.SetSelectedIds(selections);
            }

            return RedirectToAction("SelectedFlights");
        }

        [HttpGet]
        public IActionResult AddSelectedFlights(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var selections = session.GetSelections();

            if (!selections.Any(s => s.FlightId == id))
            {
                selections.Add(new FlightSelection
                {
                    FlightSelectionId = id,
                    FlightId = id
                });

                session.SetSelections(selections);
                cookies.SetSelectedIds(selections);
            }

            TempData["Message"] = "Flight Confirmed!";

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

            var selections = session.GetSelections();

            if (selections == null || !selections.Any())
            {
                var ids = cookies.GetSelectedIds();

                if (ids.Length > 0)
                {
                    selections = ids.Select(id => new FlightSelection
                    {
                        FlightSelectionId = int.Parse(id),
                        FlightId = int.Parse(id)
                    }).ToList();

                    session.SetSelections(selections);
                }
                else
                {
                    selections = new List<FlightSelection>();
                }
            }

            foreach (var item in selections)
            {
                var options = new QueryOptions<Flight>
                {
                    Includes = "Airline",
                    Where = f => f.FlightId == item.FlightId
                };

                item.Flight = flightRepo.Get(options);
            }

            var model = new FlightsViewModel
            {
                FlightSelection = selections,
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
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            session.RemoveSelections();
            cookies.RemoveSelectedIds();

            TempData["Message"] =
                "All selected flights cancelled successfully!";

            return RedirectToAction("SelectedFlights");
        }

        [HttpPost]
        public IActionResult CancelSelectedFlights(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var selections = session.GetSelections();

            var item = selections
                .FirstOrDefault(s => s.FlightSelectionId == id);

            if (item != null)
            {
                selections.Remove(item);
                session.SetSelections(selections);
            }

            cookies.RemoveSelectedId(id);

            TempData["Message"] =
                "Selected Flight cancelled successfully!";

            return RedirectToAction("SelectedFlights");
        }

        public IActionResult Details(int id)
        {
            var options = new QueryOptions<Flight>
            {
                Includes = "Airline",
                Where = f => f.FlightId == id
            };

            var flight = flightRepo.Get(options);

            if (flight == null)
                return NotFound();

            var session = new FlightSessions(HttpContext.Session);

            var model = new FlightsViewModel
            {
                Flights = flight,
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                            HttpContext.TraceIdentifier
            });
        }
    }
}