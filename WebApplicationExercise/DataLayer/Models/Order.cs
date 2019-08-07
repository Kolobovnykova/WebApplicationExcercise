using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationExercise.DataLayer.Models
{
    public class Order : Entity
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Customer name length must be between 3 and 50 characters.")]
        public string Customer { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Total must be positive.")]
        public double Total { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}