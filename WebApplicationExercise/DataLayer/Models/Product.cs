using System.ComponentModel.DataAnnotations;

namespace WebApplicationExercise.DataLayer.Models
{
    public class Product : Entity
    {
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be positive.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be positive.")]
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
    }
}