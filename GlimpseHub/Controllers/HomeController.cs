using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GlimpseHub.CloudService;
using GlimpseHub.Contracts;
using GlimpseHub.Data;
using GlimpseHub.Data.Models;
using GlimpseHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GlimpseHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbSeeder dbInitializer;
        private readonly ICloudineryService cloudService;
        private readonly ApplicationDbContext dbContext;



        public HomeController(ILogger<HomeController> logger, IDbSeeder dbInitializer, ICloudineryService cloudService, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbInitializer = dbInitializer;
            this.cloudService = cloudService;
            this.dbContext = dbContext;

        }

        public async Task<IActionResult> Index()
        {
            if (!await dbInitializer.DBHasDataAsync())
            {
                await dbInitializer.SeedAllDataAsync();
            }
            List<Picture> pictures = dbContext.Pictures.ToList();
            return View(pictures);
          
          
        }

        public async Task<IActionResult> Privacy()
        {
           
            return View();
        }
        public  IActionResult Contacts()
        {

            return View();
        }
        public  IActionResult PostPicture()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostPictureAsync(Picture picture, IFormFile file)
        { 
            string picURL = cloudService.Upload(file);
            picture.PictureUrl = picURL;
            if (ModelState.IsValid)
            {
                dbContext.Add(picture);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
