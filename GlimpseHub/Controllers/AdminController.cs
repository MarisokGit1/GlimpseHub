using GlimpseHub.CloudService;
using GlimpseHub.Data.Models;
using GlimpseHub.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GlimpseHub.Data.Models.Enum;

namespace GlimpseHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<AppUser> userManager;
        private readonly ICloudineryService cloudinary;

        public AdminController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            dbcontext = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var galFound = dbcontext.Galleries.Where(g => g.Status == Status.Pending).ToList();
            return View(galFound);
        }
        public IActionResult ChangeStatus(int galId,Status status)
        {

            return RedirectToAction("Index");
        }
        
    }
}
