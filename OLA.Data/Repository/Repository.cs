using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OLA.Data.Models;
using OLA.Data.DataContext;

namespace OLA.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }
        public async Task<int> GetCount()
            => await _entities.CountAsync();

        public async Task<T> Add(T entity)
            => (await _entities.AddAsync(entity)).Entity;

        public async Task<T> Delete(T entity)
            => _entities.Remove(entity).Entity;

        public async Task<T> Update(T entity)
        {
            var d = _context.Entry(entity);
            d.State = EntityState.Modified;
            return d.Entity;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _entities.ToListAsync();

        public async Task<DbSet<T>> GetDbSet()
            => _context.Set<T>();

        public async Task<T> GetByIdAsync(Guid id)
            => await _entities.SingleOrDefaultAsync(s => s.Id == id);

        public async Task<bool> SaveChangesAsync(Guid userId)
            => await _context.SaveChangesAsync(userId) > 0;
    }
}

