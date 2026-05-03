using Group2Flight.Models.DomainModels;

namespace Group2Flight.Models.ViewModels
{
    public class FlightsViewModel
    {
        public string ActiveFromKey { get; set; } = "all";
        public string ActiveToKey { get; set; } = "all";
        public string ActiveDepartureDate { get; set; } = "all";
        public string ActiveCabinType { get; set; } = "all";
        public List<string> CabinTypes { get; set; } = new List<string>();
        public List<Flight> Flight { get; set; } = new List<Flight>();
        public List<FlightSelection> FlightSelection { get; set; } = new List<FlightSelection>();
        public Flight Flights { get; set; } = new Flight();
        public List<string> FromCities { get; set; } = new();
        public List<string> ToCities { get; set; } = new();

        public string CheckActiveFrom(string d) =>
            d.ToLower() == ActiveFromKey.ToLower() ? "active" : "";
        public string CheckActiveTo(string d) =>
            d.ToLower() == ActiveToKey.ToLower() ? "active" : "";
        public string CheckActiveCabinType(string d) =>
            d.ToLower() == ActiveCabinType.ToLower() ? "active" : "";
    }
}
