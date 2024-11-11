using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Category)), Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product>? products { get; set; }
    }
}
