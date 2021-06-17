using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models; 

namespace MyShop.Core.viewModel
{
    public class ProductManagerViewModel
    {
        //Storing Product Object 
        public Product Product { get; set; } 

        //IEnumerable contain the list of 'ProductCategory' to iterate through
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
