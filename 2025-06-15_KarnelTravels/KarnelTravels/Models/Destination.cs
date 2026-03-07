using System.ComponentModel.DataAnnotations.Schema;

namespace KarnelTravels.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public int Price { get; set; }

        public string? DestinationLink { get; set; }

        public string? Days { get; set; }

        public string? ImagePath { get; set; }
    }
}
