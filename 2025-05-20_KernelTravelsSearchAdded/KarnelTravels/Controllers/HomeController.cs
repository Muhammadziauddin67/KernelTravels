using System.Diagnostics;
using KarnelTravels.Models;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravels.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Hotel()
        {
            return View();
        }
      
        public IActionResult Destination()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

    }
}
