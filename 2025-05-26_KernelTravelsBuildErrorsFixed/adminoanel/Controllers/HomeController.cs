using System.Diagnostics;
using adminoanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminoanel.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Iconfontawesome()
        {
            return View();
        }
        
        public IActionResult mapgoogle()
        {
            return View();
        }
        
        public IActionResult pages()
        {
            return View();
        }
        
        public IActionResult pageerror()
        {
            return View();
        }
        
        public IActionResult pageprofile()
        {
            return View();
        }
         public IActionResult tablebasic()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
