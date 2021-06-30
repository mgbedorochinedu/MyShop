using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.viewModel
{
    public class BasketItemViewModel
    {
        public string Id { get; set; } //The ID of the basket item to fetch
        public int Quantity { get; set; } 
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}

