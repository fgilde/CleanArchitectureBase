using System;
using System.Linq;

namespace CleanArchitectureBase.Application.Contracts.Data
{
    public interface IRepository<T> : IQueryable<T>
        where T : class
    {
        Lazy<int> Insert(T entity);
        void Update(int id, object values);
        void Delete(T entity);
        void Delete(int id);
        void DeleteAll();
    }
}
