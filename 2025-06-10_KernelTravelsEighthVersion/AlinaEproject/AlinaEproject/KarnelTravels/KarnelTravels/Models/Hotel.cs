using System.ComponentModel.DataAnnotations.Schema;

namespace KarnelTravels.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public int Price { get; set; }

        public string? HotelLink { get; set; }

        public string? Days { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public double Rating { get; set; }
    }
}
