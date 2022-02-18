using Microsoft.EntityFrameworkCore;
using Second.DataAccess.ApplicationDb;
using Second.Model;
using System.Collections.Generic;

namespace Second.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> table = null;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public GenericRepository(string applicationDbConn)
        {
            _context = new ApplicationDbContext(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), applicationDbConn).UseLazyLoadingProxies().Options);
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToListAsync().GetAwaiter().GetResult();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
