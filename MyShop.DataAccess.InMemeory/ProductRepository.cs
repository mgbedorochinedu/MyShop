using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; 
using MyShop.Core.Models; 

namespace MyShop.DataAccess.InMemeory
{
    public class ProductRepository
    {
        //Create an Object Cache
        ObjectCache cache = MemoryCache.Default;
        //Create internal list of product
        List<Product> products;

        //Create Constructor that will do a standard initializaton
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null) 
            {
                products = new List<Product>(); 
            }
        }
        //Create a Commit method 
        public void Commit()
        {
            cache["products"] = products;
        }

        //Below will create individual endpoints to Insert, Find, Delete, Edit and perhaps return the entire collections

        //For Insert Method
        public void Insert(Product p)
        {
            products.Add(p);
        }
        //For Update Method
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        //For Find Method
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
        //Returning a list of products
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        //For Delete Method
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
    }
}
