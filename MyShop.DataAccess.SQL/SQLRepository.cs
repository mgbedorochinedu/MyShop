using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity 
    {
        //To make this SQLrepository work, we inject our 'DataContext' class & 'DbSet' Entity
        internal DataContext context;
        internal DbSet<T> dbSet; 

        //Create a Constructor that allow us to pass in DataContext
        public SQLRepository(DataContext context)
        {
            this.context = context; 
            this.dbSet = context.Set<T>(); 
        }

        //For lIst
        public IQueryable<T> Collection()
        {
            return dbSet; 
        }

        //For Commit
        public void Commit()
        {
            context.SaveChanges(); 
        }

        //For deleting 
        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        //For Find
        public T Find(string Id)
        {
            return dbSet.Find(Id); 
        }

        //For Insert
        public void Insert(T t)
        {
            dbSet.Add(t); 
        }

        //For Uodate
        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified; 
        }
    }
}
