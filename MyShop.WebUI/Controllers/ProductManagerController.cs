using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using MyShop.Core.Models; 
using MyShop.DataAccess.InMemeory; 

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //Create an instance of the Product repository
        ProductRepository context;

        //Create a Constructor to initialize the product repository
        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //We want Index page to return the list of products which is on the collection
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //GET: Add method to create products. NB: This method will only display the page
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        //POST: Add method to create products. NB: This method will be to let details posted in
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index"); 
            }
        }

        //GET: Add Edit method
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        //POST: Add Edit method
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid) 
                {
                    return View(product);
                }
                //This manually Edit the product
                productToEdit.Categories = product.Categories;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                //Commit changes 
                context.Commit();
                //Redirect to Index Page
                return RedirectToAction("Index");

            }
        }
//......................................................................................................................................................................................
        //GET: Add Delete method
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        //POST: Add Delete method
        [HttpPost]
        [ActionName("Delete")] //Gave it alternative action name - delete
        public ActionResult ConfirmDelete(Product product, string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit(); //Delete method should commit those changes after deletion
                return RedirectToAction("Index");
            }
        }
    }
}