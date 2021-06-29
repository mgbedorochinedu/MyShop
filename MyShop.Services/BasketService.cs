using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        //Access IRepository of Product & Basket
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        //Create a string, thou not required. It is used to reference the cookie
        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.productContext = ProductContext;
            this.basketContext = BasketContext;
        }

         //It read the user cookie from the HttpContext, looking for the BasketID then it read that BasketId in the DB
        private Basket GetBasket(HttpContextBase httpContext, bool createdIfNull) 
        {
            //Read the cookie
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName); 

            //We create a new basket
            Basket basket = new Basket();

            //Check to see if the 'cookie' exist 
            if (cookie != null) 
            {
                string basketId = cookie.Value; 

                if (!string.IsNullOrEmpty(basketId)) 
                {
                    //We load the basket from the basketContext
                    basket = basketContext.Find(basketId);
                }
                else
                { //If the 'basketId' is null, we check if we want to create a new basket
                    if(createdIfNull)
                    {
                        basket = CreateNewBasket(httpContext); 
                    }
                }
            }
            else 
            {
                if (createdIfNull)
                {
                    basket = CreateNewBasket(httpContext); 
                }
            }
            return basket; 
        }

        //We create the CreateNewBasket method here
        private  Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            //We then write a cookie to the users machine
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id; 

            //We add an expiration to the cookie
            cookie.Expires = DateTime.Now.AddDays(1);

            //We Add the 'cookie' to HttpContext response
            httpContext.Response.Cookies.Add(cookie);

            //Return the basket we just created
            return basket;
        }

        //We create another method which add the basket item
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true); 
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                //Creating new items
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId, 
                    Quantity = 1 
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }
            basketContext.Commit();
        }

        //RemoveFromBasket method
        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
            if(item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
    }
}





