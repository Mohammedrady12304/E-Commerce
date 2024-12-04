using E_Commerce1.ECommerce.Infrastructure.Data;
using E_Commerce1.ViewModels;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            
            _context = context;   
        }
        

        public async Task AddAsync(Product entity)
        {
            await _context.products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

       

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool isDeleted = false;
            var product = await _context.products.FindAsync(id);
            if(product is null)
            {
                return isDeleted;
            }
            _context.products.Remove(product);
            var effectedRows = await _context.SaveChangesAsync();
            if (effectedRows > 0)
            isDeleted = true;
             

           
            return isDeleted;
                          
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.products.SingleOrDefaultAsync(p => p.Id ==id);
            return product;
        }

        public  async Task UpdateAsync(Product New)
        {
            //todo:possiple problem here
            var product = await _context.products.Include(p => p.categoryType).ThenInclude(ct => ct.Category).SingleOrDefaultAsync(p=>p.Id==New.Id);
            product = New;
            _context.products.Update(product);
           await _context.SaveChangesAsync();
     
        }
       
        public IEnumerable<SelectListItem> GetCategories() {
            return  _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }


        public IEnumerable<SelectListItem> GetCategoriesTypes() {

            return _context.CategoryTypes.Select(ct => new SelectListItem {Value=ct.Id.ToString(),Text=ct.Name }).ToList();
        }
        public async Task<IFormFile> ConvertByteArrayToFormFile(byte[] image)
        {
            
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

        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
             return null;

                   using (var memoryStream = new MemoryStream())
                   {
                       file.CopyToAsync(memoryStream);
                       return memoryStream.ToArray();
                   }
        }


        //async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return null;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file.CopyToAsync(memoryStream);
        //        return  memoryStream.ToArray();
        //    }
        //}

        //async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return null;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file.CopyToAsync(memoryStream);
        //        return memoryStream.ToArray();
        //    }
        //}
    }
}
