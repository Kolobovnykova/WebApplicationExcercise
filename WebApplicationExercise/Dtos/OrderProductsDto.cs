using System;
using System.Collections.Generic;

namespace WebApplicationExercise.Dtos
{
    public class OrderProductsDto
    {
        public Guid OrderId { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }
}