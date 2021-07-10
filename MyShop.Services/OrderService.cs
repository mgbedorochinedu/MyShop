using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;

        //Contructor to initialze the IRepository<Order>
        public OrderService(IRepository<Order> OrderContext) 
        {
            this.orderContext = OrderContext;
        }

        //We implement the IOrderService interface 
        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            //We iterate through our basket item
            foreach(var item in basketItems)
            {
                //For each 'basketItems' order, we add it to the underline 'baseOrder'
                baseOrder.OrderItems.Add(new OrderItem() 
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                }); 
            }
            //Insert and commit changes 
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }
    }
}
