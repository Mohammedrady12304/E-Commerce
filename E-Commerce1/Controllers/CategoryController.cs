using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly ILogger<Category> _logger;

        public CategoryController(IBaseRepository<Category> categoryRepository, ILogger<Category> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryRepository.GetAllAsync();
                return View(categories);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                bool result = await _categoryRepository.AddAsync(category);
                if(result)
                {
                    return RedirectToAction(nameof(Index), "Category");
                }
                _logger.LogError("Error: in adding this category");
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                bool result = await _categoryRepository.UpdateAsync(category);
                if(result)
                {
                    return RedirectToAction(nameof(Index), "Category");
                }
                _logger.LogError("Error: in adding this category");
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _categoryRepository.DeleteAsync(id);
            return result ? RedirectToAction(nameof(Index), "Category") : NotFound() ;
        }
    }
}
