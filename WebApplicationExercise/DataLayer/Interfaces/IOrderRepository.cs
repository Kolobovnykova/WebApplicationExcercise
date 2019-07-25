using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Dtos;

namespace WebApplicationExercise.DataLayer.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrders(DateTime? from = null, DateTime? to = null, string customerName = null);
        Task<Order> AssignProducts(OrderProductsDto orderProductsDto);
    }
}
