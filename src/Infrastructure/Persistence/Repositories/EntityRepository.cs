using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : Entity
    {
        protected readonly Context context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return context;
            }
        }

        public EntityRepository(Context c)
        {
            context = c;
        }

        public virtual T Add(T entity)
        {
            return context.Set<T>().Add(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual void SoftDelete(T entity)
        {
            entity.Delete();
            Update(entity);
        }

        public virtual Task<T> FindByIdAsync(long id)
        {
            return context.Set<T>()
                .Where(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public virtual Task<T> FindByConditionAsync(Expression<Func<T, bool>> conditions)
        {
            return context.Set<T>()
                 .Where(conditions)
                 .FirstOrDefaultAsync();
        }

        public virtual Task<List<T>> ListAsync()
        {
            return context.Set<T>()
                //.AsNoTracking()
                .ToListAsync();
        }

        public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> conditions)
        {
            return context.Set<T>()
                .Where(conditions)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> conditions, int pageIndex, int pageSize)
        {
            return context.Set<T>()
                .Where(conditions)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual T Update(T entity)
        {
            entity.Update();
            return context.Set<T>()
                .Update(entity).Entity;
        }

        public virtual Task<List<T>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }
    }
}
