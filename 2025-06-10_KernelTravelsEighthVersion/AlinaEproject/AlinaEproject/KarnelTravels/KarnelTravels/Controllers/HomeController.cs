using System.Diagnostics;
//using KarnelTravels.Migrations;
using KarnelTravels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KarnelTravels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;  // add this

        // Constructor injection of DbContext
        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Results(
     string DestinationName, string DestinationLocation, string DestinationDays, int? DestinationMaxPrice,
     string HotelName, string HotelLocation, double? HotelMinRating, int? HotelMaxPrice,
     string TourismName, string TourismLocation, string TourismCountry, int? TourismMinRating,
     string RestaurantName, string RestaurantDescription, int? RestaurantDay, string RestaurantYearMonth)
        {
            var vm = new SearchViewModel();

            // Destination search
            if (!string.IsNullOrEmpty(DestinationName) || !string.IsNullOrEmpty(DestinationLocation) ||
                !string.IsNullOrEmpty(DestinationDays) || DestinationMaxPrice.HasValue)
            {
                var destinations = _context.Destinations.AsQueryable();
                if (!string.IsNullOrEmpty(DestinationName))
                    destinations = destinations.Where(d => d.Name.Contains(DestinationName));
                if (!string.IsNullOrEmpty(DestinationLocation))
                    destinations = destinations.Where(d => d.Location.Contains(DestinationLocation));
                if (!string.IsNullOrEmpty(DestinationDays))
                    destinations = destinations.Where(d => d.Days.Contains(DestinationDays));
                if (DestinationMaxPrice.HasValue)
                    destinations = destinations.Where(d => d.Price <= DestinationMaxPrice.Value);
                vm.Destinations = destinations.ToList();
            }
            else
            {
                vm.Destinations = new List<Destination>();
            }

            // Hotel search
            if (!string.IsNullOrEmpty(HotelName) || !string.IsNullOrEmpty(HotelLocation) ||
                HotelMinRating.HasValue || HotelMaxPrice.HasValue)
            {
                var hotels = _context.Hotels.AsQueryable();
                if (!string.IsNullOrEmpty(HotelName))
                    hotels = hotels.Where(h => h.Name.Contains(HotelName));
                if (!string.IsNullOrEmpty(HotelLocation))
                    hotels = hotels.Where(h => h.Location.Contains(HotelLocation));
                if (HotelMinRating.HasValue)
                    hotels = hotels.Where(h => h.Rating >= HotelMinRating.Value);
                if (HotelMaxPrice.HasValue)
                    hotels = hotels.Where(h => h.Price <= HotelMaxPrice.Value);
                vm.Hotels = hotels.ToList();
            }
            else
            {
                vm.Hotels = new List<Hotel>();
            }

            // Tourism search
            if (!string.IsNullOrEmpty(TourismName) || !string.IsNullOrEmpty(TourismLocation) ||
                !string.IsNullOrEmpty(TourismCountry) || TourismMinRating.HasValue)
            {
                var tourisms = _context.Tourisms.AsQueryable();
                if (!string.IsNullOrEmpty(TourismName))
                    tourisms = tourisms.Where(t => t.Name.Contains(TourismName));
                if (!string.IsNullOrEmpty(TourismLocation))
                    tourisms = tourisms.Where(t => t.Location.Contains(TourismLocation));
                if (!string.IsNullOrEmpty(TourismCountry))
                    tourisms = tourisms.Where(t => t.Country.Contains(TourismCountry));
                if (TourismMinRating.HasValue)
                    tourisms = tourisms.Where(t => t.Rating >= TourismMinRating.Value);
                vm.Tourisms = tourisms.ToList();
            }
            else
            {
                vm.Tourisms = new List<Tourism>();
            }

            // Restaurant search
            if (!string.IsNullOrEmpty(RestaurantName) || !string.IsNullOrEmpty(RestaurantDescription) ||
                RestaurantDay.HasValue || !string.IsNullOrEmpty(RestaurantYearMonth))
            {
                var restaurants = _context.Restaurants.AsQueryable();
                if (!string.IsNullOrEmpty(RestaurantName))
                    restaurants = restaurants.Where(r => r.Name.Contains(RestaurantName));
                if (!string.IsNullOrEmpty(RestaurantDescription))
                    restaurants = restaurants.Where(r => r.Description.Contains(RestaurantDescription));
                if (RestaurantDay.HasValue)
                    restaurants = restaurants.Where(r => r.Day == RestaurantDay.Value);
                if (!string.IsNullOrEmpty(RestaurantYearMonth))
                    restaurants = restaurants.Where(r => r.yearmonth.Contains(RestaurantYearMonth));
                vm.Restaurants = restaurants.ToList();
            }
            else
            {
                vm.Restaurants = new List<Restaurant>();
            }

            return View(vm);
        }
        public IActionResult tourismspots()
        {
            var tourisms = _context.Tourisms.ToList();
            return View(tourisms);        
        }

        public IActionResult Hotel(string destination, string hotelName, string days, double? minRating, decimal? maxPrice)
        {
            var hotels = _context.Hotels.AsQueryable();

            // Destination (Location or Name)
            if (!string.IsNullOrEmpty(destination))
            {
                hotels = hotels.Where(h => h.Location != null && h.Location.Contains(destination));
            }

            // Hotel Name (Exact or Partial Match)
            if (!string.IsNullOrEmpty(hotelName))
            {
                hotels = hotels.Where(h => h.Name != null && h.Name.Contains(hotelName));
            }

            // Days
            if (!string.IsNullOrEmpty(days))
            {
                hotels = hotels.Where(h => h.Days != null && h.Days.Contains(days));
            }

            // Min Rating
            if (minRating.HasValue)
            {
                hotels = hotels.Where(h => h.Rating >= minRating.Value);
            }

            // Max Price
            if (maxPrice.HasValue)
            {
                hotels = hotels.Where(h => h.Price <= maxPrice.Value);
            }

            return View(hotels.ToList());
        }

        public IActionResult Destination(string destinationName, string days, string location, decimal? maxPrice)
        {
            var destinations = _context.Destinations.AsQueryable();

            // Destination Name (Exact or Partial Match)
            if (!string.IsNullOrEmpty(destinationName))
            {
                destinations = destinations.Where(d => d.Name != null && d.Name.Contains(destinationName));
            }
            // Destination (Location)
            if (!string.IsNullOrEmpty(location))
            {
                destinations = destinations.Where(d => d.Location != null && d.Location.Contains(location));
            }
            // Days
            if (!string.IsNullOrEmpty(days))
            {
                destinations = destinations.Where(d => d.Days != null && d.Days.Contains(days));
            }
            // Price
            if (maxPrice.HasValue)
            {
                destinations = destinations.Where(d => d.Price <= maxPrice.Value);
            }

            return View(destinations.ToList());
        }
        public IActionResult Resort()
        {
            return View();
        }


        public IActionResult Restaurant()
        {

            var restaurants = _context.Restaurants.ToList();
            return View(restaurants);
       
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
       
    }
}
