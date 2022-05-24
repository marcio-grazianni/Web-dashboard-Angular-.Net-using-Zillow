using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface IEntityRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        Task<T> FindByIdAsync(long id);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> conditions);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> conditions);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> conditions, int pageIndex, int pageSize);
        Task<List<T>> SearchAsync(string query);
    }
}