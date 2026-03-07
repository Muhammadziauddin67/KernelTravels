using System.ComponentModel.DataAnnotations.Schema;

namespace KarnelTravels.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        public string? ImagePath { get; set; }

        public string? Name { get; set; }

        public int Day { get; set; }

        public string? yearmonth { get; set; }

        public string? Description { get; set; }

        public string? RestaurantLink { get; set; }

    }
}
