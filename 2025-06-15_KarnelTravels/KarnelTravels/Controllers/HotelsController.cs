using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KarnelTravels.Models;

namespace KarnelTravels.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ApplicationDBContext _context;

        private IWebHostEnvironment env;

        public HotelsController(ApplicationDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel, IFormFile ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null)
                {
                    var FolderName = Path.Combine(env.WebRootPath, "assets", "images");
                    var FileName = Guid.NewGuid() + ImagePath.FileName;
                var CompleteAddress = Path.Combine(FolderName, FileName);
                var F = new FileStream(CompleteAddress, FileMode.Create);
                ImagePath.CopyTo(F);
                hotel.ImagePath = FileName;
                }
                await _context.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Edit/5


        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, int HotelId, Hotel hotel, IFormFile ImagePath)
        //{
        //    if (id != hotel.HotelId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (ImagePath != null)
        //            {
        //                var FolderName = Path.Combine(env.WebRootPath, "assets", "images");
        //                var FileName = Guid.NewGuid() + ImagePath.FileName;
        //                var CompleteAddress = Path.Combine(FolderName, FileName);
        //                var F = new FileStream(CompleteAddress, FileMode.Create);
        //                ImagePath.CopyTo(F);
        //                hotel.ImagePath = FileName;
        //            }
        //            else
        //            {
        //                var oldclient = await _context.Hotels.AsNoTracking().FirstOrDefaultAsync(c => c.HotelId == hotel.HotelId);
        //                hotel.ImagePath = oldclient?.ImagePath;
        //            }
        //            _context.Update(hotel);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!HotelExists(hotel.HotelId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(hotel);
        //}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
            //var restaurant = await _context.Restaurants.FindAsync(id);

            //if (restaurant == null)
            //    return NotFound();

            //return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId,Name,Location,Price,HotelLink,Days,Description,ImagePath,Rating")] Hotel hotel, IFormFile ImagePath)
        {
            if (id != hotel.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Image upload logic
                    if (ImagePath != null)
                    {
                        var folderPath = Path.Combine(env.WebRootPath, "assets", "images");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImagePath.CopyToAsync(stream);
                        }

                        hotel.ImagePath = fileName;
                    }
                    else
                    {
                        // Keep old image if no new one is uploaded
                        var oldHotel = await _context.Hotels.AsNoTracking().FirstOrDefaultAsync(h => h.HotelId == hotel.HotelId);
                        hotel.ImagePath = oldHotel?.ImagePath;
                    }

                    _context.Update(hotel);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.HotelId))
                        return NotFound();
                    else
                        throw;
                }
            }

            // For debugging invalid model state
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }
    }
}
