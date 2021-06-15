using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        //Creating basic products information
        public string Id { get; set; }

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Categories { get; set; }
        public string Image { get; set; }
        //Create a Constructor so that everytime we create an instance pf Product, it automatically generate an ID instead from the DB
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
