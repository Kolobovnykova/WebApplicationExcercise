using System;
using System.Collections.Generic;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Models
{
    public class Order : Entity
    {
        public DateTime CreatedDate { get; set; }

        public string Customer { get; set; }

        public List<Product> Products { get; set; }
    }
}