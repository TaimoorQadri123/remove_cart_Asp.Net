using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelHandling.Data;
using ModelHandling.Models;

namespace ModelHandling.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
    

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var std = _context.Students.ToList();
            return View(std);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]

        public async Task<IActionResult> Create(Students std)
        {
            if(ModelState.IsValid)
            {
                await _context.Students.AddAsync(std);
                await _context.SaveChangesAsync();
                ViewBag.Message = $"Student {std.Name} addedd Successfully";


            }
          return View(std);
        }

        [HttpGet]
        public IActionResult Update( int id)
        {
            var std = _context.Students.FirstOrDefault(x => x.Id == id);
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Students std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(std);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var del = await _context.Students.FindAsync(id);
            _context.Students.Remove(del);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
