using Microsoft.AspNetCore.Mvc;

namespace PlantEShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Product");
        }
    }
    
}
