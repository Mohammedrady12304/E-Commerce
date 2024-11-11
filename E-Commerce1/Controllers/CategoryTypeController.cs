using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce1.Controllers
{
    public class CategoryTypeController : Controller
    {
        private readonly IBaseRepository<CategoryType> _categoryTypeRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly ILogger<CategoryType> _logger;

        public CategoryTypeController(IBaseRepository<CategoryType> categoryTypeRepository, ILogger<CategoryType> logger, 
                                      IBaseRepository<Category> categoryRepository)
        {
            _categoryTypeRepository = categoryTypeRepository;
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryTypeRepository.GetAllAsync();
                return View(categories);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryType categoryType)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryType.CategoryId);
                categoryType.Category = category;
                bool result = await _categoryTypeRepository.AddAsync(categoryType);
                if (result)
                {
                    return RedirectToAction(nameof(Index), "CategoryType");
                }
                _logger.LogError($"Error: in adding this type {categoryType.Name} in this category {categoryType.Category!.Name}");
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(categoryType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoryType = await _categoryTypeRepository.GetByIdAsync(id);
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryType.Category!.Id);
            return View(categoryType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryType categoryType)
        {
            if (ModelState.IsValid)
            {
                bool result = await _categoryTypeRepository.UpdateAsync(categoryType);
                if (result)
                {
                    return RedirectToAction(nameof(Index), "CategoryType");
                }
                _logger.LogError($"Error: in updating this type {categoryType.Name} in this category {categoryType.Category!.Name}");
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryType.Category!.Id);
            return View(categoryType);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _categoryTypeRepository.DeleteAsync(id);
            return result ? RedirectToAction(nameof(Index), "CategoryType") : NotFound();
        }
    }
}
