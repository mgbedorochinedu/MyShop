using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        //Adding Basic details on OrderItem
        public string OrderId { get; set; } 
        public string ProductId { get; set; } 
        public string ProductName { get; set; } 
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
