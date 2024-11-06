using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Type
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> products { get; set; }
        public Category Category { get; set; }
    }
}
