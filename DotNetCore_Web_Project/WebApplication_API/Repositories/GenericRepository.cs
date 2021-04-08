using HelperClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;

//基础类
namespace WebApplication_API.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected ContosoContext _dbContext;

        protected GenericRepository(ContosoContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public async virtual Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }


        public virtual IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public virtual IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<T> results = query.Where(predicate).ToList();
            return results;
        }


        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public IEnumerable<T> GetPagedList(out int totalCount, int? page = null, int? pageSize = null,
    Expression<Func<T, bool>> filter = null, string[] includePaths = null,
    params SortExpression<T>[] sortExpressions)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = _dbSet.Where(filter);

            totalCount = query.Count();

            if (includePaths != null)
                for (var i = 0; i < includePaths.Count(); i++)
                    query = query.Include(includePaths[i]);

            if (sortExpressions != null)
            {
                IOrderedQueryable<T> orderedQuery = null;
                for (var i = 0; i < sortExpressions.Count(); i++)
                    if (i == 0)
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
                        else
                            orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                        else
                            orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                    }

                if (page != null)
                    query = orderedQuery.Skip(((int)page - 1) * (int)pageSize);
            }


            if (pageSize != null)
                query = query.Take((int)pageSize);

            return query.ToList();
        }
        public IEnumerable<U> GetBy<U>(Expression<Func<T, U>> columns, Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).Select(columns);
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var queryable = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, property) => current.Include(property));
        }
    }
}