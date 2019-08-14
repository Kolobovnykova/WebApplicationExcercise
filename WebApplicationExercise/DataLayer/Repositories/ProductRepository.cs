using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MainDataContext _context;

        public ProductRepository(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Product> Get(Guid productId)
        {
            var product = await _context.Products.Include(x => x.Order)
                .SingleOrDefaultAsync(o => o.Id == productId);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(x => x.Order).ToListAsync();
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
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            var result = await Get(product.Id);
            if (result == null)
            {
                throw new NotFoundException(nameof(product));
            }

            result.Name = product.Name;
            result.Price = product.Price;
            result.Quantity = product.Quantity;

            _context.Entry(result.Order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Product> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}