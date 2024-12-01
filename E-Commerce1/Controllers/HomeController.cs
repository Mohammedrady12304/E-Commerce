using E_Commerce1.Models;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryTypeRepository _categoryType;
        public HomeController(ILogger<HomeController> logger, ICategoryTypeRepository categoryType)
        {
            _logger = logger;
            _categoryType = categoryType;
        }

        public async Task<IActionResult> Index()
        {
            var categoryTypes = await _categoryType.GetAllAsync();
            return View(categoryTypes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
