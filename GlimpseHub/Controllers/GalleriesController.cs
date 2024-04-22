using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlimpseHub.Data;
using GlimpseHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using GlimpseHub.CloudService;
using GlimpseHub.Data.Models.Enum;

namespace GlimpseHub.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly ICloudineryService cloudinary;

        public GalleriesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Galleries
        public async Task<IActionResult> Index(string type)
        {

            List<Gallery> galleriesFound = new List<Gallery>();


            switch (type)
            {
                case "closed":
                    {
                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).Where(g => g.Status == Status.Closed).ToListAsync();
                        break;
                    }
                case "personal":
                    {
                        var currentUserId = userManager.GetUserId(User);
                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).Where(g => g.AppUserId == currentUserId).ToListAsync();
                        break;
                    }
                
                case "pending":
                    {
                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).Where(g => g.Status == Status.Pending).ToListAsync();
                        break;
                    }
                case "deleted":
                    {
                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).Where(x => x.IsDeleted).ToListAsync();
                        break;
                    }
                case "most-wanted":
                    {
                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).Take(3).ToListAsync();
                        break;
                    }
                default: 

                        galleriesFound = await _context.Galleries.Include(g => g.Author).Include(g => g.Category).ToListAsync();
                    break;
            }
              return View(galleriesFound);
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !await _context.Galleries.AnyAsync())
            {
                return NotFound();
            }

            Gallery gallery = await _context.Galleries
                .Include(g => g.Author)
                .Include(g => g.Category)
                .Include(g=>g.Pictures)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            var currentUser = userManager.GetUserAsync(User).GetAwaiter().GetResult();

            ViewData["AppUserId"] = currentUser.Id;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Galleries/Create
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,IsPrivate,AppUserId,CategoryId,Id,Name,CreatedOn,IsDeleted")] Gallery gallery, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", gallery.AppUserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", gallery.CategoryId);

            string picURL = cloudinary.Upload(file);
            using (var fs = new FileStream(picURL, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            var currentUser = userManager.GetUserAsync(User).GetAwaiter().GetResult();

            ViewData["AppUserId"] = currentUser.Id;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", gallery.CategoryId);
            return View(gallery);
        }

        // POST: Galleries/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,IsPrivate,AppUserId,CategoryId,Id,Name,CreatedOn,IsDeleted")] Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", gallery.AppUserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", gallery.CategoryId);
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .Include(g => g.Author)
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galleries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Galleries'  is null.");
            }
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            return (_context.Galleries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
