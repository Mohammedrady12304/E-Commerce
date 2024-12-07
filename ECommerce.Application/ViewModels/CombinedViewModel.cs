using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.ViewModels
{
    public class CombinedViewModel
    {
        public IEnumerable <Product> products { get; set; }
        public IEnumerable< ApplicationUser> applicationUsers{get;set;}
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<CategoryType> categoryTypes { get; set; }
    }
}
