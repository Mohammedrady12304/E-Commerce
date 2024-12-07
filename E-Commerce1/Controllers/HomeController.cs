using E_Commerce1.Models;
using ECommerce.Application.ViewModels;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryTypeRepository _categoryType;
        private readonly IProductRepository _productRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(ILogger<HomeController> logger, ICategoryTypeRepository categoryType, IProductRepository productRepository, IApplicationUserRepository applicationUserRepository , ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryType = categoryType;
            _productRepository = productRepository;
            _applicationUserRepository = applicationUserRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            CombinedViewModel combined = new()
            {
                products = await _productRepository.GetAllAsync(),
                applicationUsers = await _applicationUserRepository.GetAllAsync(),
                categories = await _categoryRepository.GetAllAsync(),
                categoryTypes = await _categoryType.GetAllAsync()
            };
           
            return View(combined);
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
