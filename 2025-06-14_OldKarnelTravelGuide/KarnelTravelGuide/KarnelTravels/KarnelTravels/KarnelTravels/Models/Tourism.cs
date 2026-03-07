using System.ComponentModel.DataAnnotations.Schema;

namespace KarnelTravels.Models
{
    public class Tourism
    {
        public int TourismId { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public int Price { get; set; }

        public string? Days { get; set; }

        public int Rating { get; set; }

        public string? Country { get; set; }

        public string? TourismLink { get; set; }

        public string? ImagePath { get; set; }
    }
}
