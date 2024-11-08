using Microsoft.AspNetCore.Mvc;

namespace E_Commerce1.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
