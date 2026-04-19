using Group2Flight.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group2Flight.Controllers
{
    public class ValidationController : Controller
    {
        private Group2FlightDatabaseContext context;
        public ValidationController(Group2FlightDatabaseContext ctx) => context = ctx;

        public JsonResult CheckFlight(string FlightCode, DateTime Date)
        {
            string msg = Check.FlightCodeDateExists(context, Date, FlightCode);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okFlightDate"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
