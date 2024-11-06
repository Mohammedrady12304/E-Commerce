using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Review ProductReview { get; set; }
       
        public ICollection<Product> products { get; set; }

        public WishList wishList { get; set; }
        public Cart cart { get; set; }
        public ICollection<Payout> payouts { get; set; }
        public ICollection<Earnings> earnings { get; set; }

    }
}
