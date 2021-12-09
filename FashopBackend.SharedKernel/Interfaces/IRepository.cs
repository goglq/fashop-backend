using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.SharedKernel.Interfaces
{
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        T Get(int id);

        Task Create(T entity);

        void Update(T entity);

        void Remove(T entity);

        void Save();

        Task SaveAsync();
    }
}
