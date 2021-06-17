using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching; 
using MyShop.Core.Models;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemeory
{
    public class ProductCategoryRepository 
    {
        //Create an Object Cache
        ObjectCache cache = MemoryCache.Default;
        //Create internal list of product
        List<ProductCategory> ProductCategories;

        //Create Constructor that will do a standard initializaton
        public ProductCategoryRepository()
        {
            ProductCategories = cache["ProductCategories"] as List<ProductCategory>;
            if (ProductCategories == null)
            {
                ProductCategories = new List<ProductCategory>();
            }
        }
        //Create a Commit method 
        public void Commit()
        {
            cache["ProductCategories"] = ProductCategories;
        }

        //Below will create individual endpoints to Insert, Find, Delete, Edit and perhaps return the entire collections

        //For Insert Method
        public void Insert(ProductCategory p)
        {
            ProductCategories.Add(p);
        }
        //For Update Method
        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductCategoryToUpdate = ProductCategories.Find(p => p.Id == productCategory.Id);

            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        //For Find Method
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = ProductCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        //Returning a list of products
        public IQueryable<ProductCategory> Collection()
        {
            return ProductCategories.AsQueryable();
        }
        //For Delete Method
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = ProductCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                ProductCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}
