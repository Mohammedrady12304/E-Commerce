using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace E_Commerce1.ViewModels
{
    public class AddProductViewModel 
    {
        [MaxLength(250)]
        public string ProductName { get; set; } = string.Empty;
        [Display(Name ="Category")]
        public int categoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }=Enumerable.Empty<SelectListItem>();
        [Display(Name = "Type")]
        public int CategoryTypeId { get; set; }
        public IEnumerable<SelectListItem> CategoryTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public decimal Price { get; set; } 
        [MaxLength(250)]
        public string Model { get; set; }=string.Empty;
        [MaxLength(250)]
        public string Brand { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public IFormFile? productImages { get; set; } 


    }
}
