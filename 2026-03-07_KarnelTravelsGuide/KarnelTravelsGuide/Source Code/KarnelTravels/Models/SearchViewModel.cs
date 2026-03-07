using System.ComponentModel.DataAnnotations.Schema;
using KarnelTravels.Models;
namespace KarnelTravels.Models
{
    public class SearchViewModel
    {
        public List<Destination> Destinations { get; set; } = new List<Destination>();
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        public List<Tourism> Tourisms { get; set; } = new List<Tourism>();
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}