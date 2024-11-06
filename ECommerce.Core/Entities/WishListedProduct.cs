using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class WishListedProduct
    {
        public int WishListedProductId { get; set; }
        public DateTime AddedDate { get; set; }
        public WishList wishList { get; set; }
        public Product product { get; set; }
    }
}
