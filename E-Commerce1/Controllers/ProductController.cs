using E_Commerce1.ViewModels;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
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
                categoryTypeId=AddproductVM.CategoryTypeId,
                Name = AddproductVM.ProductName,
                Description = AddproductVM.ProductDescription,
                Brand = AddproductVM.Brand,
                Price = AddproductVM.Price,
                Model = AddproductVM.Model,
                Images =await _product.ConvertIFormFileToByteArray(AddproductVM.productImages)

            };
           await _product.AddAsync(product);
            //todo: search for method to send the id with product details here to redirct to the product after adding it directly
            return RedirectToAction("Index","Home");
            
        }
        [HttpGet]
        //todo:here is an problem in update the view model take the old data and send it
        public async Task<IActionResult> EditProduct(int id)
        {
            
            var product =await _product.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }


            EditProductViewModel EditProductVM = new()
            {

                ProductId = product.Id,
                Categories = _product.GetCategories(),
                CategoryTypes = _product.GetCategoriesTypes(),
                ProductName = product.Name,
                categoryId = product.categoryType.Category.Id,
                CategoryTypeId = product.categoryTypeId,
                Price = product.Price,
                Model = product.Model,
                Brand = product.Brand,
                ProductDescription = product.Description,
                
                productImages = ConvertByteArrayToFormFile(product.Images)


            };
            return View(EditProductVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> EditProduct(EditProductViewModel EditproductVM)
        {
            var product = await _product.GetByIdAsync(EditproductVM.ProductId);
            if (!ModelState.IsValid)
            {
                
                EditproductVM.Categories = _product.GetCategories();
                EditproductVM.Categories = _product.GetCategoriesTypes();

                return View(EditproductVM);
            }
            
            

            product.Id = EditproductVM.ProductId;
            product.categoryTypeId = EditproductVM.CategoryTypeId;
            product.Name = EditproductVM.ProductName;
            product.Description = EditproductVM.ProductDescription;
            product.Brand = EditproductVM.Brand;
            product.Price = EditproductVM.Price;
            product.Model = EditproductVM.Model;
            if (EditproductVM.productImages != null && EditproductVM.productImages.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await EditproductVM.productImages.CopyToAsync(memoryStream);
                product.Images = memoryStream.ToArray();
            }
            else
            {
                product.Images ??= product.Images;
            }


            await _product.UpdateAsync(product);
            return RedirectToAction("Index", "Home"); ;
            
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _product.DeleteAsync(id);
            
            return isDeleted ? Ok() : BadRequest();
            
        }
        public IFormFile ConvertByteArrayToFormFile(byte[]? image)
        {
            if(image == null)
            
                return null;
            

            using (var memoryStream = new MemoryStream(image))
            {
                string fileName = "defaultFileName.jpg";


                IFormFile formFile = new FormFile(memoryStream, 0, image.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                };

                return formFile;
            }


        }

        public byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<IActionResult> GetProductPicture(int id)
        {
            var product =await  _product.GetByIdAsync(id);
            if (product?.Images == null)
            {
                return File("/images/default-profile.png", "image/png");
            }

            return File(product.Images, "image/jpeg");
        }


    }
}
