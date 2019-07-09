using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MainDataContext _context;
        private readonly CustomerManager _customerManager;

        [InjectionConstructor]
        public OrderRepository(MainDataContext context, CustomerManager customerManager)
        {
            _context = context;
            _customerManager = customerManager;
        }

        public async Task<Order> Get(Guid orderId)
        {
            var order = await _context.Orders.Include(o => o.Products).SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(order));
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders(DateTime? from = null, DateTime? to = null, string customerName = null)
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

        public async Task Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new NotFoundException(nameof(order));
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
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