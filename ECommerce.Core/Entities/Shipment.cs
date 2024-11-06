using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public string DestinationAddress {  get; set; }
        public string SourceAddress { get; set; }
        public decimal Cost { get; set; }
        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public Order Order { get; set; }
    }
}
