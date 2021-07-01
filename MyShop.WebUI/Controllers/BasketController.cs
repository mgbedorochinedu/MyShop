using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        //We use the IBasketService to expose the formatted of the IBasketService
        IBasketService basketService;

        //Constructor that allows us to inject in the BasketService
        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            //We use the Index page to return our basket view which is a list of all our basket items
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        //Endpoint for adding to Basket
        public ActionResult AddToBasket(string Id) //AddToBasket takes in a ProductId & pass that through to the basketService
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index"); //After adding, we redirect to Action - "Index" page
        }

        //Endpoint for Removing from Basket
        public ActionResult RemoveFromBasket(string Id) //RemoveToBasket takes in a ProductId & pass that through to the basketService
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index"); //After removing, we redirect to Action - "Index" page
        }

        //The 'BasketSummary' endpoint will be a PartialViewResult
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
    }
}