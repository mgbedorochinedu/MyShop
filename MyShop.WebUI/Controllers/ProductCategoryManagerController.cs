using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemeory; 

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context; 

        //Injecting the classes through the Constructor to initialize. 
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context; 
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            //We want Index page to return the list of Product Categories which is on the collection
            List<ProductCategory> productCategory = context.Collection().ToList();
            return View(productCategory);
        }

        //GET: Add method to create Product Categories. NB: This method will only display the page
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        //POST: Add method to create Product Categories. NB: This method will be to let details posted in
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //GET: Add Edit method
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        //POST: Add Edit method
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                //This manually Edit the Product Category
                productCategoryToEdit.Category = productCategory.Category;
   
                //Commit changes 
                context.Commit();
                //Redirect to Index Page
                return RedirectToAction("Index");

            }
        }
        //GET: Add Delete method
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        //POST: Add Delete method
        [HttpPost]
        [ActionName("Delete")] //Gave it alternative action name - delete
        public ActionResult ConfirmDelete(Product productCategory, string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit(); 
                return RedirectToAction("Index");
            }
        }
    }
}