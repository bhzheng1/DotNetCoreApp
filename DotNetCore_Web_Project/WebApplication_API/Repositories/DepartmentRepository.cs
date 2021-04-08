using System;
using System.Threading.Tasks;
using WebApplication_API.ContosoEntities;
using WebApplication_API.DbContexts;

//本例演示如何使用Transaction
namespace WebApplication_API.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly ContosoContext _contosoContext;
        public DepartmentRepository(ContosoContext contosoContext)
        {
            _contosoContext = contosoContext;
        }

        public Task<int> CreateDepartment(Department department) {
            using (var tran = _contosoContext.Database.BeginTransaction())
            {
                try
                {
                    var dept = _contosoContext.Departments.Add(department).Entity;
                    var beforeSave = dept.Id;
                    _contosoContext.SaveChanges();
                    var afterSave = dept.Id;
                    tran.Commit();
                    return Task.FromResult(dept.Id);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Exception encountered: {ex.Message}");
                    tran.Rollback();
                    throw;
                }
            }
        }  
    }
}
