using MariosSpeciality.Models;
using Microsoft.AspNetCore.Mvc;
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
            model["topThreeLates"] = GetTopThreeLatestProducts();
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

        private List<Product> GetTopThreeProducts()
        {
            var products = (from p in db.Products
                            orderby p.AverageRating()
                            select p).Take(3);
            return products.ToList();
        }
        private List<Product> GetTopThreeLatestProducts()
        {
            var products = (from p in db.Products
                            orderby p.DatePosted descending
                            select p).Take(3);
            return products.ToList();
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
