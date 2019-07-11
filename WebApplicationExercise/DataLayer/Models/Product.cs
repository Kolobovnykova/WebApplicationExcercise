using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationExercise.DataLayer.Models
{
    public class Product : Entity
    {
        [StringLength(50, ErrorMessage = "The Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The Price must be positive.")]
        public double Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}