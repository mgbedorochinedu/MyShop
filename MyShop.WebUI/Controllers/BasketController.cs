using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller 
    {
        IRepository<Customer> customers; 
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers) 
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers; 
        }
      
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        //Endpoint for adding to Basket
        public ActionResult AddToBasket(string Id) 
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        //Endpoint for Removing from Basket
        public ActionResult RemoveFromBasket(string Id) 
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index"); 
        }

        // 'BasketSummary' endpoint will be a PartialViewResult
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        //For Checkout page
        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            //Making sure the Customer is not null
            if(customer != null)
            {
                //Create and new Order & prefill some of the Order with the users details
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    ZipCode = customer.ZipCode
                };
                return View(order); 
            }
            else 
            {
                return RedirectToAction("Error"); 
            }          
        }

        //POST - Checkout Page
        [HttpPost]
        [Authorize]        
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext); 
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name; 
            //Process Payment  

            order.OrderStatus = "Payment Processed"; 
            orderService.CreateOrder(order, basketItems); 
            basketService.ClearBasket(this.HttpContext); 
            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        //Create Thank you page
        public ActionResult ThankYou(string OrderId) 
        {
            ViewBag.OrderId = OrderId; 
            return View(); 
        }
    }
}