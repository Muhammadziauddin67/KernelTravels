using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KarnelTravels.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravels.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _env;

        public RestaurantsController(ApplicationDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurants.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant, IFormFile ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null)
                {
                    var folder = Path.Combine(_env.WebRootPath, "assets", "images");
                    var fileName = Guid.NewGuid() + Path.GetExtension(ImagePath.FileName);
                    var fullPath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ImagePath.CopyToAsync(stream);
                    }

                    restaurant.ImagePath = fileName;
                }

                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurant restaurant, IFormFile ImagePath)
        {
            if (id != restaurant.RestaurantId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImagePath != null)
                    {
                        var folder = Path.Combine(_env.WebRootPath, "assets", "images");
                        var fileName = Guid.NewGuid() + Path.GetExtension(ImagePath.FileName);
                        var fullPath = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await ImagePath.CopyToAsync(stream);
                        }

                        restaurant.ImagePath = fileName;
                    }
                    else
                    {
                        var existing = await _context.Restaurants.AsNoTracking()
                            .FirstOrDefaultAsync(r => r.RestaurantId == restaurant.RestaurantId);

                        restaurant.ImagePath = existing?.ImagePath;
                    }

                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }
    }
}
