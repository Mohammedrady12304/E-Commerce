using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Payment payment { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
        public Shipment shipment { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
