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

        IBasketService basketService;
        IOrderService orderService; 

        public BasketController(IBasketService BasketService, IOrderService OrderService) 
        {
            this.basketService = BasketService;
            this.orderService = OrderService; 
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
        public ActionResult Checkout()
        {
            return View();
        }

        //POST - Checkout Page
        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext); 

            order.OrderStatus = "Order Created"; 

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