using System.ComponentModel.DataAnnotations;

namespace Group2Flight.Models.DomainModels
{
    public class Airline
    {
        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a ImageName.")]
        public string ImageName { get; set; } = string.Empty;
    }
}
