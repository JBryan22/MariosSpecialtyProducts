using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MariosSpeciality.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MariosSpeciality.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _context;

        public ReviewsController(IReviewRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                _context = new EFReviewRepository();   
            }  
            else
            {
                _context = thisRepo;
            }
        }

        // GET: Reviews
        public IActionResult Index()
        {
            var mariosDbContext = _context.Reviews.Include(r => r.Product);
            return View(mariosDbContext.ToList());
        }

        // GET: Reviews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _context.Reviews
                .Include(r => r.Product)
                .SingleOrDefault(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int? productId)
        {
            ViewBag.ProductId = productId;
            //ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ReviewId,Author,ContntBody,Rating,AuthorImg,ProductId")] Review review, ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
                        review.AuthorImg = fileBytes;
                    }
                }
            }
            if (ModelState.IsValid)
            {
                _context.Save(review);
                return RedirectToAction("Index");
            }
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            return RedirectToAction("Index", "Products");
        }
        public IActionResult RedirectToProduct(int productId)
        {
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        // GET: Reviews/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _context.Reviews.SingleOrDefault(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ReviewId,Author,ContntBody,Rating,AuthorImg,ProductId")] Review review)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _context.Reviews
                .Include(r => r.Product)
                .SingleOrDefault(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = _context.Reviews.SingleOrDefault(m => m.ReviewId == id);
            _context.Remove(review);
            return RedirectToAction("Index");
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
