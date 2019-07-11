using System;
using System.Collections.Generic;

namespace WebApplicationExercise.DataLayer.Models
{
    public class Order : Entity
    {
        public DateTime CreatedDate { get; set; }

        public string Customer { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}