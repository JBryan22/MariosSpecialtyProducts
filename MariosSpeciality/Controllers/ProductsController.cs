using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MariosSpeciality.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace MariosSpeciality.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _context;

        public ProductsController(IProductRepository thisRepo = null )
        {
            if (thisRepo == null)
            {
                _context = new EFProductRepository();
            }
            else
            {
                _context = thisRepo;
            }
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.Reviews).ToList());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .Include(p => p.Reviews)
                .SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,Name,Cost,CountryOfOrigin,ProductImg")] Product product, ICollection<IFormFile> files = null)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] fileBytes = ms.ToArray();
                            product.ProductImg = fileBytes;
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Save(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProductId,Name,Cost,CountryOfOrigin,ProductImg")] Product product, ICollection<IFormFile> files = null)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] fileBytes = ms.ToArray();
                            product.ProductImg = fileBytes;
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =_context.Products
                .SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.SingleOrDefault(m => m.ProductId == id);
            _context.Remove(product);
            return RedirectToAction("Index");
        }

        public IActionResult CreateReview(int id)
        {
            return RedirectToAction("Create", "Reviews", new { productId = id });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
