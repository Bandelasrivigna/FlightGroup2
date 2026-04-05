using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group2Flight.Models
{
    public class FlightSelection
    {
        public int FlightSelectionId { get; set; }
        public int FlightId { get; set; }
        [ValidateNever]
        public Flight Flight { get; set; } = null!;
    }
}
