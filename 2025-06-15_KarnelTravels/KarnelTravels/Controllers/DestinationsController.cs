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
    public class DestinationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        private IWebHostEnvironment env;

        public DestinationsController(ApplicationDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Destinations.ToListAsync());
        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations
                .FirstOrDefaultAsync(m => m.DestinationId == id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // GET: Destinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Destination destination, IFormFile ImagePath)
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
                    destination.ImagePath = FileName;
                }
                await _context.AddAsync(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int DestinationId, Destination destination, IFormFile ImagePath)
        {
            if (id != destination.DestinationId)
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
                        destination.ImagePath = FileName;
                    }
                    else
                    {
                        var oldclient = await _context.Destinations.AsNoTracking().FirstOrDefaultAsync(c => c.DestinationId == destination.DestinationId);
                        destination.ImagePath = oldclient?.ImagePath;
                    }
                    _context.Update(destination);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.DestinationId))
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
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations
                .FirstOrDefaultAsync(m => m.DestinationId == id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinationExists(int id)
        {
            return _context.Destinations.Any(e => e.DestinationId == id);
        }
    }
}
