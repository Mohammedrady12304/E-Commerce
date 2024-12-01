using E_Commerce1.ViewModels;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Drawing.Drawing2D;

namespace E_Commerce1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _product;
        public ProductController(IProductRepository product)
        {
            _product = product;
        }

        public async Task<IActionResult> productDetails(int id)
        {
            var product =await _product.GetByIdAsync(id);
            return View("productDetails",product);
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            AddProductViewModel addProductVM = new()
            {
               
                Categories = _product.GetCategories(),
                CategoryTypes = _product.GetCategoriesTypes()
            };
            return View(addProductVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductViewModel AddproductVM)
        {
            
            AddproductVM.Categories = _product.GetCategories();
            AddproductVM.Categories = _product.GetCategoriesTypes();

            if (!ModelState.IsValid) 
            { 
                return View(AddproductVM);
            }
           
            
                Product product = new()
            {
                Name = AddproductVM.ProductName,
                Description = AddproductVM.ProductDescription,
                Brand = AddproductVM.Brand,
                Price = AddproductVM.Price,
                Model = AddproductVM.Model,
                Images =await _product.ConvertIFormFileToByteArray(AddproductVM.productImages)

            };
           await _product.AddAsync(product);
            //todo: search for method to send the id with product details here to redirct to the product after adding it directly
            return RedirectToAction(nameof(productDetails));
            
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            
            var product =await _product.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
           
            EditProductViewModel EditProductVM = new EditProductViewModel
            {
                
                Categories = _product.GetCategories(),
                CategoryTypes = _product.GetCategoriesTypes(),
                ProductName = product.Name,
                categoryId = product.Id,
                CategoryTypeId = product.categoryType.Id,
                Price = product.Price,
                Model=product.Model,
                Brand=product.Brand,
                ProductDescription=product.Description,
                productImages=await _product.ConvertByteArrayToFormFile(product.Images),
            };
            return View(EditProductVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //todo:now we are here in the edit product
        public async Task<IActionResult> EditProduct(EditProductViewModel EditproductVM)
        {
            if (!ModelState.IsValid)
            {
                
                EditproductVM.Categories = _product.GetCategories();
                EditproductVM.Categories = _product.GetCategoriesTypes();

                return View(EditproductVM);
            }
            
            Product product = new()
            {
                Name = EditproductVM.ProductName,
                Description = EditproductVM.ProductDescription,
                Brand = EditproductVM.Brand,
                Price = EditproductVM.Price,
                Model = EditproductVM.Model,
                Images = await _product.ConvertIFormFileToByteArray(EditproductVM.productImages)

            };
            await _product.UpdateAsync(product);
            return RedirectToAction(nameof(productDetails));
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            //todo:don't forget to handle Delete alert
            bool isDeleted = await _product.DeleteAsync(id);
            return isDeleted ? Ok() : BadRequest();    
        }


    }
}
