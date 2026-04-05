using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Group2Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please enter a FlightCode.")]
        public string FlightCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a From.")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a To.")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter a DepartureTime.")]
        public TimeSpan DepartureTime { get; set; }

        [Required(ErrorMessage = "Please enter a ArrivalTime.")]
        public TimeSpan ArrivalTime { get; set; }

        [Required(ErrorMessage = "Please enter a CabinType.")]
        public string CabinType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Emission.")]
        public double Emission { get; set; }

        [Required(ErrorMessage = "Please enter a AircraftType.")]
        public string AircraftType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Price.")]
        public int Price { get; set; }
        public int AirlineId { get; set; }
        [ValidateNever]
        public Airline Airline { get; set; } = null!;
    }
}
