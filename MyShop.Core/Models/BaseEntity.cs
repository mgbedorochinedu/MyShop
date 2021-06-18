using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    //Created BaseEntity class that contain ID & CreatedDate
    public abstract class BaseEntity 
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        //Create a Constructor to initialze it
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString(); 
            this.CreatedAt = DateTime.Now;
    }
    }
}
