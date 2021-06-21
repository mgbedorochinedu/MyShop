using MyShop.Core.Models; 
using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    //DataContext class inherits from a data Entity Framework called DbContext
    public class DataContext : DbContext 
    {
        //We create a Constructor so that we can capture and pass in that connection string that the base class is expecting
        public DataContext()
            : base("DefaultConnection") 
        {
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductCategory> ProductCategories {get; set;} 

    }
}
