using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public string BasketId { get; set; } //Create BasketId which will be a link to the Basket that contain BasketItems 
        public string ProductId { get; set; }  //We will also have a link to the ProductId
        public int Quantity { get; set; } 
    }
}
