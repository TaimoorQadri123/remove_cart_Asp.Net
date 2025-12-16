using Microsoft.AspNetCore.Mvc;

namespace ModelHandling.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
