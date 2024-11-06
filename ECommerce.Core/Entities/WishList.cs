using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class WishList
    {
        public int Id { get; set; }
      
        public string userId { get; set; }
        [ForeignKey("userId")]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
