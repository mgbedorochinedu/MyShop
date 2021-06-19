using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
using MyShop.Core.Contracts; 

namespace MyShop.DataAccess.InMemeory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity 
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        //Create a Generic for Commit method that stored items in memory
        public void Commit()
        {
            cache[className] = items;
        }

        //Create a Generic for Insert method 
        public void Insert(T t)
        {
            items.Add(t);
        }

        //Create a Generic for Update method 
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        //Create a Generic for Find method 
        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        //Create Generic that Returns a list of all the collection
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        //Create a Generic for Delete method 
        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}



















