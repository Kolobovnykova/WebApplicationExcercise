using System;
using System.Threading.Tasks;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.DataLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update (TEntity entity);
        Task<TEntity> Delete(Guid id);
    }
}
