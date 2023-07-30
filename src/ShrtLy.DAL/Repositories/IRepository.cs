using System.Collections.Generic;
using ShrtLy.DAL.Entities;

namespace ShrtLy.DAL.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        int Create(T entity);

        IEnumerable<T> GetAll();
    }
}