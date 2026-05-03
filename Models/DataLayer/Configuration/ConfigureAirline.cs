using Group2Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group2Flight.Models
{
    internal class ConfigureAirline : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> entity)
        {
            // seed initial data
            entity.HasData(
                new Airline { AirlineId = 1, Name = "Emirates Airlines", ImageName = "emirates_airlines.png" },
                new Airline { AirlineId = 2, Name = "Qatar Airways", ImageName = "qatar_airways.png" },
                new Airline { AirlineId = 3, Name = "Singapore Airlines", ImageName = "singapore_airlines.png" },
                new Airline { AirlineId = 4, Name = "Lufthansa", ImageName = "lufthansa.png" },
                new Airline { AirlineId = 5, Name = "Air France", ImageName = "air_france.png" },
                new Airline { AirlineId = 6, Name = "British Airways", ImageName = "british_airways.png" }
            );
        }
    }

}
