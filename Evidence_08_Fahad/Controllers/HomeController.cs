using Microsoft.AspNetCore.Mvc;

namespace Evidence_08_Fahad.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
