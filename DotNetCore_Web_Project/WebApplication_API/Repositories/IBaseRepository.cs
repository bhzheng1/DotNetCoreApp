using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication_API.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IQueryable<T>> FindAll();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<bool> Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
    }
}
