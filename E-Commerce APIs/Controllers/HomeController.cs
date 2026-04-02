using Microsoft.AspNetCore.Mvc;

namespace lab11.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
