using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelHandling.Data;
using ModelHandling.Models;

namespace ModelHandling.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env=env;
        }
        [Authorize(Roles ="Admin")]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.Category);
            return View(await appDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [Authorize(Roles = "Admin")]

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CatName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductCatDescription,Price,ImageUrl,CategoryId")] Products products,IFormFile file)
        {
            ModelState.Remove("ImageUrl");

            if (!ModelState.IsValid)
            {
                return View(products);
            }
            if (file == null || file.Length == 0)
            {
                ViewBag.Error = "Please Upload an Image";
                return View(products);
            }

            string extension = Path.GetExtension(file.FileName).ToLower();
            var allowedextension = new[] { ".jpg", ".png", ".jpeg" };
            if(!allowedextension.Contains(extension))
            {
                ViewBag.Error = "Only jpg,png,jpeg images allowed";
                return View(products);
            }

            string filestore = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(filestore))
            {
                Directory.CreateDirectory(filestore);
            }

                string filename = Guid.NewGuid().ToString()+extension;
                string filepath = Path.Combine(filestore, filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                products.ImageUrl = @$"\uploads\{filename}";


            _context.Add(products);
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CatName", products.CategoryId);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CatName", products.CategoryId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductCatDescription,Price,ImageUrl,CategoryId")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CatName", products.CategoryId);
            return View(products);
        }
        [Authorize(Roles ="Admin")]

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

    }
}
