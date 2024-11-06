using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string userId { get; set; }
        [ForeignKey("userId")]
        public ApplicationUser User { get; set; }
        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }
    }
}
