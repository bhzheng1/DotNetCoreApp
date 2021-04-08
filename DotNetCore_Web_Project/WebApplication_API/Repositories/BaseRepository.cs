using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;

//基础类
namespace WebApplication_API.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected SakilaContextMSSQL _dbContext { get; set; }

        public BaseRepository(SakilaContextMSSQL _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IQueryable<T>> FindAll()
        {
            var result = await _dbContext.Set<T>().ToListAsync();
            return result.AsQueryable();
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var result = await _dbContext.Set<T>().Where(expression).ToListAsync();
            return result.AsQueryable();
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Task Update(T entity)
        {
            //in case AsNoTracking is used
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
