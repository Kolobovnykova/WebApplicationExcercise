using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Dtos;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MainDataContext _context;
        private readonly CustomerManager _customerManager;
        private readonly IRepository<Product> _productRepository;

        public OrderRepository(MainDataContext context, CustomerManager customerManager, IRepository<Product> productRepository)
        {
            _context = context;
            _customerManager = customerManager;
            _productRepository = productRepository;
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

        public async Task<Order> Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> Create(Order order, Guid[] productIds)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }

            if (productIds == null || !productIds.Any())
            {
                var result = await Create(order);
                return result;
            }

            foreach (var prodId in productIds)
            {
                var result = await _productRepository.Get(prodId);
                if (result != null)
                {
                    order.Products.Add(result);
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }

            var resultOrder = await Get(order.Id);
            if (resultOrder == null)
            {
                throw new NotFoundException(nameof(order));
            }

            resultOrder.Customer = order.Customer;
            resultOrder.CreatedDate = order.CreatedDate;
            resultOrder.Products = order.Products;

            await _context.SaveChangesAsync();

            return resultOrder;
        }

        public async Task<Order> AssignProducts(OrderProductIdsDto orderProductIdsDto)
        {
            if (orderProductIdsDto == null)
            {
                throw new ArgumentNullException();
            }

            var order = await Get(orderProductIdsDto.OrderId);
            if (orderProductIdsDto.ProductIds.Any())
            {
                foreach (var productId in orderProductIdsDto.ProductIds)
                {
                    var product = await _productRepository.Get(productId);
                    order.Products.Add(product);
                }
            }

            await _context.SaveChangesAsync();

            return order;
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