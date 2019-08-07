using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.DataLayer.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
