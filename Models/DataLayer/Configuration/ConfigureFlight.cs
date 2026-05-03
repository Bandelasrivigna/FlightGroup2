using Group2Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group2Flight.Models
{
    internal class ConfigureFlight : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> entity)
        {
            // seed initial data
            entity.HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightCode = "EK206",
                    From = "Dubai",
                    To = "Sydney",
                    Date = new DateTime(2026, 5, 15),
                    DepartureTime = new TimeSpan(2, 30, 0),
                    ArrivalTime = new TimeSpan(22, 15, 0),
                    CabinType = "Economy",
                    Emission = 850.3,
                    AircraftType = "Airbus A380",
                    Price = 2500,
                    AirlineId = 1,
                },
                new Flight
                {
                    FlightId = 2,
                    FlightCode = "QR123",
                    From = "Doha",
                    To = "Paris",
                    Date = new DateTime(2026, 5, 18),
                    DepartureTime = new TimeSpan(7, 45, 0),
                    ArrivalTime = new TimeSpan(13, 30, 0),
                    CabinType = "Business",
                    Emission = 320.8,
                    AircraftType = "Boeing 787 Dreamliner",
                    Price = 890,
                    AirlineId = 2,
                },
                new Flight
                {
                    FlightId = 3,
                    FlightCode = "SQ345",
                    From = "Singapore",
                    To = "Tokyo",
                    Date = new DateTime(2026, 5, 22),
                    DepartureTime = new TimeSpan(9, 15, 0),
                    ArrivalTime = new TimeSpan(17, 45, 0),
                    CabinType = "Economy Plus",
                    Emission = 280.4,
                    AircraftType = "Airbus A350",
                    Price = 680,
                    AirlineId = 3,
                },
                new Flight
                {
                    FlightId = 4,
                    FlightCode = "LH456",
                    From = "Frankfurt",
                    To = "Mumbai",
                    Date = new DateTime(2026, 5, 25),
                    DepartureTime = new TimeSpan(13, 50, 0),
                    ArrivalTime = new TimeSpan(1, 20, 0),
                    CabinType = "Economy",
                    Emission = 410.5,
                    AircraftType = "Airbus A340",
                    Price = 540,
                    AirlineId = 4,
                },
                new Flight
                {
                    FlightId = 5,
                    FlightCode = "AF789",
                    From = "Paris",
                    To = "Rio de Janeiro",
                    Date = new DateTime(2026, 5, 28),
                    DepartureTime = new TimeSpan(23, 15, 0),
                    ArrivalTime = new TimeSpan(6, 45, 0),
                    CabinType = "Business",
                    Emission = 620.7,
                    AircraftType = "Boeing 777-300ER",
                    Price = 1350,
                    AirlineId = 5,
                },
                new Flight
                {
                    FlightId = 6,
                    FlightCode = "BA890",
                    From = "London",
                    To = "Cape Town",
                    Date = new DateTime(2026, 6, 1),
                    DepartureTime = new TimeSpan(18, 30, 0),
                    ArrivalTime = new TimeSpan(7, 15, 0),
                    CabinType = "Economy Plus",
                    Emission = 710.2,
                    AircraftType = "Airbus A380",
                    Price = 980,
                    AirlineId = 6,
                },
                new Flight
                {
                    FlightId = 7,
                    FlightCode = "EK567",
                    From = "Dubai",
                    To = "Miami",
                    Date = new DateTime(2026, 6, 5),
                    DepartureTime = new TimeSpan(3, 45, 0),
                    ArrivalTime = new TimeSpan(10, 30, 0),
                    CabinType = "Business",
                    Emission = 780.5,
                    AircraftType = "Boeing 777-200LR",
                    Price = 2100,
                    AirlineId = 1,
                },
                new Flight
                {
                    FlightId = 8,
                    FlightCode = "QR234",
                    From = "Doha",
                    To = "Bangkok",
                    Date = new DateTime(2026, 6, 8),
                    DepartureTime = new TimeSpan(1, 20, 0),
                    ArrivalTime = new TimeSpan(12, 45, 0),
                    CabinType = "Economy",
                    Emission = 350.9,
                    AircraftType = "Airbus A330",
                    Price = 470,
                    AirlineId = 2,
                },
                new Flight
                {
                    FlightId = 9,
                    FlightCode = "SQ789",
                    From = "Singapore",
                    To = "Melbourne",
                    Date = new DateTime(2026, 6, 12),
                    DepartureTime = new TimeSpan(22, 40, 0),
                    ArrivalTime = new TimeSpan(8, 20, 0),
                    CabinType = "Business",
                    Emission = 490.2,
                    AircraftType = "Airbus A350-900",
                    Price = 1120,
                    AirlineId = 3,
                },
                new Flight
                {
                    FlightId = 10,
                    FlightCode = "LH567",
                    From = "Munich",
                    To = "Shanghai",
                    Date = new DateTime(2026, 6, 15),
                    DepartureTime = new TimeSpan(15, 10, 0),
                    ArrivalTime = new TimeSpan(9, 30, 0),
                    CabinType = "Economy Plus",
                    Emission = 550.3,
                    AircraftType = "Boeing 747-8",
                    Price = 780,
                    AirlineId = 1,
                }
            );
        }
    }

}
