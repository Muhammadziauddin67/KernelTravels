using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarnelTravels.Migrations;
using KarnelTravels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravels.Controllers
{
    public class TourismController : Controller
    {
        private readonly ApplicationDBContext _context;

        private IWebHostEnvironment env;

        public TourismController(ApplicationDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Tourism
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tourisms.ToListAsync());
        }

        // GET: Tourism/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourism = await _context.Tourisms
                .FirstOrDefaultAsync(m => m.TourismId == id);
            if (tourism == null)
            {
                return NotFound();
            }

            return View(tourism);
        }

        // GET: Tourism/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tourism/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Tourism tourism, IFormFile ImagePath)
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
                    tourism.ImagePath = FileName;
                }
                await _context.AddAsync(tourism);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourism);
        }

        // GET: Tourism/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourism = await _context.Tourisms.FindAsync(id);
            if (tourism == null)
            {
                return NotFound();
            }
            return View(tourism);
        }

        // POST: Tourism/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int TourismId, Tourism tourism, IFormFile ImagePath)
        {
            if (id != tourism.TourismId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImagePath != null)
                    {
                        var FolderName = Path.Combine(env.WebRootPath, "assets", "images");
                        var FileName = Guid.NewGuid() + ImagePath.FileName;
                        var CompleteAddress = Path.Combine(FolderName, FileName);
                        var F = new FileStream(CompleteAddress, FileMode.Create);
                        ImagePath.CopyTo(F);
                        tourism.ImagePath = FileName;
                    }
                    else
                    {
                        var oldclient = await _context.Tourisms.AsNoTracking().FirstOrDefaultAsync(c => c.TourismId == tourism.TourismId);
                        tourism.ImagePath = oldclient?.ImagePath;
                    }
                    _context.Update(tourism);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourismExists(tourism.TourismId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tourism);
        }

        // GET: Tourism/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourism = await _context.Tourisms
                .FirstOrDefaultAsync(m => m.TourismId == id);
            if (tourism == null)
            {
                return NotFound();
            }

            return View(tourism);
        }

        // POST: Tourism/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourism = await _context.Tourisms.FindAsync(id);
            if (tourism != null)
            {
                _context.Tourisms.Remove(tourism);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourismExists(int id)
        {
            return _context.Tourisms.Any(e => e.TourismId == id);
        }
    }
}
