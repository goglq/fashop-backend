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
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly FashopContext _context;

        protected DbSet<T> DbSet { get; }

        public RepositoryBase(FashopContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return filter == null ? DbSet.ToList() : DbSet.Where(filter).ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public async Task Create(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
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
