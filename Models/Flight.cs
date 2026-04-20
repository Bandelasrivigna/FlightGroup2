using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Group2Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please enter a FlightCode.")]
        [RegularExpression(@"^[A-Za-z]{2}\d{1,4}$", ErrorMessage = "FlightCode should consist of alpha and numbers, 2 letters and then 1-4 numbers.")]
        public string FlightCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a From.")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters allowed.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter only letters.")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a To.")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters allowed.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter only letters.")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Date.")]
        [LegitimateDate(3, ErrorMessage = "The date should be valid, subsequent to today and within three years.")]
        [Remote(action: "CheckFlight", controller: "Validation", areaName: "",AdditionalFields = nameof(FlightCode),ErrorMessage = "A flight for this date already exists.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter a DepartureTime.")]
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Enter time in HH:mm format (00:00 to 23:59).")]
        public TimeSpan DepartureTime { get; set; }

        [Required(ErrorMessage = "Please enter a ArrivalTime.")]
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Enter time in HH:mm format (00:00 to 23:59).")]
        public TimeSpan ArrivalTime { get; set; }

        [Required(ErrorMessage = "Please enter a CabinType.")]
        public string CabinType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Emission.")]
        [Range(0, 5000, ErrorMessage = "The amount of CO2 emissions should not surpass 5000 kg CO2e.")]
        public double Emission { get; set; }

        [Required(ErrorMessage = "Please enter a AircraftType.")]
        public string AircraftType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Price.")]
        [Range(0, 50000, ErrorMessage = "The price should be 0-50,000 USD.")]
        public int Price { get; set; }
        public int AirlineId { get; set; }
        [ValidateNever]
        public Airline Airline { get; set; } = null!;
    }
}
