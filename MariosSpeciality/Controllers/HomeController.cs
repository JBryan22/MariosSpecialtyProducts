using MariosSpeciality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MariosSpeciality.Controllers
{
    public class HomeController : Controller
    {
        EFProductRepository db = new EFProductRepository();
        public IActionResult Index()
        {
            Dictionary<string, List<Product>> model = new Dictionary<string, List<Product>>();
            model["topThreeRated"] = GetTopThreeProducts();
            model["topThreeLatest"] = GetTopThreeLatestProducts();
            ViewBag.model = model;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult GetReviewsForProduct(int? id)
        {
            var model = db.Products
                .Include(p => p.Reviews)
                .Where(r => r.ProductId == id)
                //.OrderByDescending(r => r.DatePosted.Date)
                //.ThenByDescending(r => r.DatePosted.TimeOfDay)
                .FirstOrDefault() as Product;
            return View(model);
        }

        private List<Product> GetTopThreeProducts()
        {
            var products = db.Products
                .Include(p => p.Reviews)
                .OrderByDescending(p => p.AverageRating())
                .Take(3)
                .ToList();
            return products;
        }
        private List<Product> GetTopThreeLatestProducts()
        {
            var products = db.Products
                .Include(p => p.Reviews)
                .OrderByDescending(p => p.DatePosted)
                .Take(3)
                .ToList();
            return products;
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
