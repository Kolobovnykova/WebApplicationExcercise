using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Unity;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.DataLayer.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly MainDataContext _context;

        public ProductRepository(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Product> Get(Guid productId)
        {
            var product = await _context.Products.Include(o => o.Orders)
                .SingleOrDefaultAsync(o => o.Id == productId);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            return product;
        }

        public async Task<Product> Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}