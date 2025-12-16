using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelHandling.Data;
using ModelHandling.Models;
using System.Security.Claims;

namespace ModelHandling.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = _context.Cart.FirstOrDefault(x => x.ProductId == id && x.UserId == userId);
            if (cartItem == null)
            {
                Cart cart = new Cart
                {
                    ProductId = id,
                    UserId = userId,
                    Quantity = 1
                };
                _context.Cart.Add(cart);
            }
            else
            {
                cartItem.Quantity += 1;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.Cart
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();
            return View(cart);
        }
    }
}
