using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Payout
    {
        public int Id { get; set; }
        public DateTime PayoutDate { get; set; }
        public decimal PayoutAmount { get; set; }
        public ApplicationUser User { get; set; }
    }
}
