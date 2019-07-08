using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.Models;

namespace WebApplicationExercise.DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MainDataContext _context;
        private readonly CustomerManager _customerManager;

        [Inject]
        public OrderRepository(MainDataContext context, CustomerManager customerManager)
        {
            _context = context;
            _customerManager = customerManager;
        }

        public async Task<Order> Get(Guid orderId)
        {
            return await _context.Orders.Include(o => o.Products).SingleOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(DateTime? @from = null, DateTime? to = null, string customerName = null)
        {
            IEnumerable<Order> orders = await _context.Orders.Include(o => o.Products).ToListAsync();

            if (from != null && to != null)
            {
                orders = FilterByDate(orders, from.Value, to.Value);
            }

            if (customerName != null)
            {
                orders = FilterByCustomer(orders, customerName);
            }

            return orders.Where(o => _customerManager.IsCustomerVisible(o.Customer));
        }

        public async Task Create(Order entity)
        {
            if (entity != null)
            {
                _context.Orders.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Order entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Order> FilterByCustomer(IEnumerable<Order> orders, string customerName)
        {
            return orders.Where(o => o.Customer == customerName);
        }

        private IEnumerable<Order> FilterByDate(IEnumerable<Order> orders, DateTime from, DateTime to)
        {
            return orders.Where(o => o.CreatedDate >= from && o.CreatedDate < to);
        }
    }
}