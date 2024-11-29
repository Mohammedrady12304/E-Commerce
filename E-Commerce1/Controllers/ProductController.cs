using Microsoft.AspNetCore.Mvc;

namespace E_Commerce1.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> productDetails(int id)
        {
            return View();
        }
    }
}
