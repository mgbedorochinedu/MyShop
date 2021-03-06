using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.viewModel;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>(); 

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders); 
            var controller = new BasketController(basketService, orderService, customers); 

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            //Setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>(); 

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders); 
            var controller = new BasketController(basketService, orderService, customers); 

            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);
        }

        //Created "CanCheckoutAndCreateOrder"  method
        [TestMethod]
        public void CanCheckoutAndCreateOrder() 
        {
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Customer> customers = new MockContext<Customer>();   

            //Addded basic infirmation to the product repository
            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });
            //Created a basket repository
            IRepository<Basket> baskets = new MockContext<Basket>();
            //Added a new underline basket to that
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId ="1", Quantity = 2, BasketId = basket.Id});
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1, BasketId = basket.Id });
            //Added the basket to Basket Repository
            baskets.Insert(basket);
            IBasketService basketService = new BasketService(products, baskets);
            //Created Order Service
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);

            //Adding the FakeUser to the underline context  
            customers.Insert(new Customer() { Id = "1", Email = "mgbedorochinedu@gmail.com", ZipCode = "100001" });
            //After having the user customer in the DB, we then create an IPrincipal, 
            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("mgbedorochinedu@gmail.com", "Forms"), null); 

            var controller = new BasketController(basketService, orderService, customers);    
            var httpContext = new MockHttpContext();
            
            httpContext.User = FakeUser; 
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            Order order = new Order();
            controller.Checkout(order); 

            //Assert
            Assert.AreEqual(2, order.OrderItems.Count); 
            Assert.AreEqual(0, basket.BasketItems.Count); 

            //Retrieving the Order from the Repository
            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count); 
        }
    }
}
