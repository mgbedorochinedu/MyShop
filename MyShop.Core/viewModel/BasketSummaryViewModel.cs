using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.viewModel
{
    public class BasketSummaryViewModel
    {
        //We want to get the BasketCount and BasketTotal
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }

        //We create a empty Constructor, because we want to also create one that have some default value
        public BasketSummaryViewModel()
        {

        }

        //This will be the Constructor that have Default value. The user will pass in some value on Creation
        public BasketSummaryViewModel(int basketCount, decimal basketTotal)
        {
            this.BasketCount = basketCount;
            this.BasketTotal = basketTotal;
        }
    }
}
