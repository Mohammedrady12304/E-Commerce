using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce1.ViewModels
{
    public class EditProductViewModel
    {
        [MaxLength(250)]
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(2500)]
        public int categoryId { get; set; }
        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public int CategoryTypeId { get; set; }
        [Display(Name = "Type")]
        public IEnumerable<SelectListItem> CategoryTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public decimal Price { get; set; }
        [MaxLength(250)]
        public string Model { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Brand { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public IFormFile? productImages { get; set; } 


    }
}
