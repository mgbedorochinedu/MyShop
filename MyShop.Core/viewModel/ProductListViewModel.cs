using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.viewModel
{
    //We passed in our Product list and Product Categories list to the viewModel to perform filter
    public class ProductListViewModel 
    {
        public IEnumerable<Product> Products { get; set; } 
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
