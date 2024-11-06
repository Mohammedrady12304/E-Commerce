using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public Cart Cart { get; set; }
        public Product product { get; set; }
    }
}
