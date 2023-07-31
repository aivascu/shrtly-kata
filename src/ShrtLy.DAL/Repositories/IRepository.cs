using System.Collections.Generic;
using System.Threading.Tasks;
using ShrtLy.DAL.Entities;

namespace ShrtLy.DAL.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<int> CreateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByUrl(string url);
    }
}