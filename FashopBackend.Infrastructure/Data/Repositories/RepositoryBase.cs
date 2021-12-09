using FashopBackend.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private FashopContext _context;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet => _dbSet;

        public RepositoryBase(FashopContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            if(filter == null)
                return DbSet.ToList();
            return DbSet.Where(filter).ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public async Task Create(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
