using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string Brand{ get; set; } 
        public decimal Price { get; set; } 

        public string Model { get; set; } 
        public byte[]? Images { get; set; }

        public Review Review { get; set; } = new();
        public CategoryType categoryType { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } 
        public WishList? wishList { get; set; }
        public Cart? cart { get; set; }
        public ICollection<Order>? orders { get; set; }
        
    }
}
