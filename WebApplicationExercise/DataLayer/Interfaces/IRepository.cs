using System;
using System.Threading.Tasks;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.DataLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(Guid id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> Delete(Guid id);
    }
}
