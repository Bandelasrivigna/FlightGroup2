using Group2Flight.Models;
using Group2Flight.Models.DomainModels;
using Group2Flight.Models.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group2Flight.Areas.Airlines.Controllers
{
    [Area("Airlines")]
    public class FlightsController : Controller
    {
        private IRepository<Flight> flightData { get; set; }
        private IRepository<Airline> airlineData { get; set; }

        public FlightsController(IRepository<Flight> flightRepo,
                                 IRepository<Airline> airlineRepo)
        {
            flightData = flightRepo;
            airlineData = airlineRepo;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            LoadAirlines();

            return View("Edit", new Flight());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Disable = "";

            LoadAirlines();

            var flight = flightData.Get(id);
            return View(flight);
        }

        [HttpPost]
        public IActionResult Edit(Flight flight)
        {
            if (TempData["okFlightDate"] == null)
            {
                string msg = Check.FlightCodeDateExists(
                    null, flight.Date, flight.FlightCode);

                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(flight.FlightCode), msg);
                }
            }

            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                {
                    flightData.Insert(flight);
                    TempData["Message"] = $"{flight.FlightCode} Added Successfully";
                }
                else
                {
                    flightData.Update(flight);
                    TempData["Message"] = $"{flight.FlightCode} Updated Successfully";
                }

                flightData.Save();

                return RedirectToAction("Index");
            }

            LoadAirlines();
            ViewBag.Action = (flight.FlightId == 0) ? "Add" : "Edit";

            return View(flight);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = flightData.Get(id);
            return View(flight);
        }

        [HttpPost]
        public IActionResult Delete(Flight flight)
        {
            flightData.Delete(flight);
            flightData.Save();

            TempData["Message"] = $"{flight.FlightCode} Deleted Successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var options = new QueryOptions<Flight>
            {
                Includes = "Airline"
            };

            var flights = flightData.List(options)
                                    .OrderBy(f => f.FlightCode)
                                    .ToList();

            return View(flights);
        }

        public IActionResult Manage()
        {
            return Content("Airline Manage Flights Page");
        }

        public IActionResult Regulation()
        {
            return Content("Airline Regulation Page");
        }

        private void LoadAirlines()
        {
            var options = new QueryOptions<Airline>();

            var airlines = airlineData.List(options)
                .OrderBy(a => a.AirlineId)
                .Select(a => new SelectListItem
                {
                    Value = a.AirlineId.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Airlines = airlines;
        }
    }
}