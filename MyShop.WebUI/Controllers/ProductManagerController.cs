using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts; 
using MyShop.Core.Models;
using MyShop.Core.viewModel; 
using MyShop.DataAccess.InMemeory; 

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context; 
        IRepository<ProductCategory> productCategories;

        //Injecting the classes through the Constructor to initialize. 
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext; 
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //We want Index page to return the list of products which is on the collection
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        //GET: Add Create method
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
 
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection(); 
       
            return View(viewModel);  
        }
        //POST: Add Create method
        [HttpPost]                                  
        public ActionResult Create(Product product, HttpPostedFileBase file) 
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null) 
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName); 

                     file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);  
                }
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
                ProductManagerViewModel viewModel = new ProductManagerViewModel(); 

                viewModel.Product = product; 
                viewModel.ProductCategories = productCategories.Collection();  

                return View(viewModel);  
            }
        }
  
        //POST: Add Edit method
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file) 
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
                if(file != null) 
                {
                    productToEdit.Image = productToEdit.Id + Path.GetExtension(file.FileName); 
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image); 
                }


                productToEdit.Categories = product.Categories;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                //Commit changes 
                context.Commit();
                //Redirect to Index Page
                return RedirectToAction("Index");

            }
        }

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
        [ActionName("Delete")] 
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
                context.Commit(); 
                return RedirectToAction("Index");
            }
        }
    }
}