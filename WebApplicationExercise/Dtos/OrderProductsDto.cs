using System;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Dtos
{
    public class OrderProductsDto
    {
        public Order Order { get; set; }
        public Guid[] ProductIds { get; set; }
    }
}