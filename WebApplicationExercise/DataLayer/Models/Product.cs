using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public double Price { get; set; }
    }
}