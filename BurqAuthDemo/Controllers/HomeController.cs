using Microsoft.AspNetCore.Mvc;

namespace BurqAuthDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
