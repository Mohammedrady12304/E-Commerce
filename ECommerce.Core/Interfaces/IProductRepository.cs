using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerce.Core.Interfaces
{
   public interface IProductRepository :IRepository<Product>
    {
        
         IEnumerable<SelectListItem> GetCategories();


        IEnumerable<SelectListItem> GetCategoriesTypes();
       
        IFormFile ConvertByteArrayToFormFile(byte[] image);
        Task<byte[]> ConvertIFormFileToByteArray(IFormFile file);


    }
}
