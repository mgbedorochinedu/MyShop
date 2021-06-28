using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity //We make sure it contain BaseEntity which have an ID
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; } //Here we want a list of basket items. We set it as 'virtual', this tells
                                                // E.F that whenever we load from the DB, it will automatically load all the Basket items as well.

        public Basket() //Create a Constructor that will create an empty list of basket on creation. 
        {
            this.BasketItems = new List<BasketItem>(); 
        }
    }
}
