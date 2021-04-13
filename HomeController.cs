using CafeFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace CafeFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CafeDbContext db;

        public HomeController(ILogger<HomeController> logger, CafeDbContext injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.VisitorCount = (new Random()).Next(1, 10001);
            model.Cafes = db.Cafe.OrderBy(c => c.Name).ToList();

            return View(model);
        }

        public IActionResult Directory()
        {
            var model = new HomeIndexViewModel();
            model.Cafes = db.Cafe.OrderBy(c => c.Name).ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CafeDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a Cafw ID in the route, for example, / Home / CafeDetail / 21");
            }
            var model = db.Cafe
            .SingleOrDefault(p => p.ID == id);
            if (model == null)
            {
                return NotFound($"Product with ID of {id} not found.");
            }
            return View(model); // pass model to view and then return result
        }
        [HttpPost]
        public IActionResult ModelBinding()
        {
            return View(); // the page with a form to submit
        }
        public IActionResult ModelBinding(SearchCafe thing)
        {
            // return View(thing); 
            var model = new HomeModelBindingViewModel
            {
                Thing = thing,
                HasErrors = !ModelState.IsValid,
                ValidationErrors = ModelState.Values
            .SelectMany(state => state.Errors)
            .Select(error => error.ErrorMessage)
            };
            return View(model);
        }
        public IActionResult SearchByZip(int? zip)
        {
            if (!zip.HasValue)
            {
                return NotFound("You must pass a zip code in the query string, for example, / Home / SearchByZip ? zip = 11374");
            }
            IEnumerable<Cafe> model = db.Cafe.Where(c => c.Zip == zip);

            if (model.Count() == 0)
            {
                return NotFound($"No cafes found at given zip code .");
            }
            ViewData["ZipNames"] = zip.Value.ToString("C");
            return View(model); // pass model to view
        }
        public IActionResult DeleteCafe(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass an ID in the query string, for example, / Home / DeleteCafe ? id = 15");
            }

            db.Cafe.Remove(db.Cafe.Where(c => c.ID == id).First());
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult CafeInput()
        {
            return View();
        }

        public IActionResult AddCafe(string Name, string BuildingNumber, string Street, string City, string State, int Zip, string Latitude, string Longitude)
        {
            Cafe caf = new Cafe
            {
                Name = Name,
                BuildingNumber = BuildingNumber,
                Street = Street,
                City = City,
                State = State,
                Zip = Zip,
                Latitude = Latitude,
                Longitude = Longitude
            };
            db.Cafe.Add(caf);
            db.SaveChanges();
            int ID = caf.ID;

            return RedirectToAction("CafeDetail", new { ID = ID});
        }
        
        public IActionResult AdvancedSearch()
        {
            return View();
        }

        public IActionResult SearchByLongLat(int longitude, int latitude, int radius)
        {

            IEnumerable<Cafe> query1 = db.Cafe.Where(c => Convert.ToInt32(c.Longitude) <= longitude + radius);
            IEnumerable<Cafe> query2 = query1.Where(c => Convert.ToInt32(c.Longitude) >= longitude - radius);
            IEnumerable<Cafe> query3 = query2.Where(c => Convert.ToInt32(c.Latitude) <= latitude + radius);
            IEnumerable<Cafe> model = query3.Where(c => Convert.ToInt32(c.Latitude) >= latitude - radius);

                if (model.Count() == 0)
            {
                return NotFound($"No cafes found in this zone .");
            }
            ViewData["AdvancedSearch"] = longitude.ToString("C");
            return View(model); // pass model to view
        }
        public IActionResult EditCafe(int id)
        {
            var model = db.Cafe
            .SingleOrDefault(p => p.ID == id);
            if (model == null)
            {
                return NotFound($"Product with ID of {id} not found.");
            }
            return View(model);
        }
        public IActionResult ChangeCafe(int ID, string Name, string BuildingNumber, string Street, string City, string State, int Zip, string Latitude, string Longitude)
        {

            var cafe = db.Cafe.Where(c => c.ID == ID).First();
            cafe.Name = Name;
            cafe.BuildingNumber = BuildingNumber;
            cafe.Street = Street;
            cafe.City = City;
            cafe.State = State;
            cafe.Zip = Zip;
            cafe.Latitude = Latitude;
            cafe.Longitude = Longitude;
            db.SaveChanges();

            return RedirectToAction("CafeDetail", new { ID = ID });

            //return RedirectToAction(HttpUtility.HtmlDecode($"CafeDetail/{ID}"));
        }

    }
}

