using Microsoft.EntityFrameworkCore;
using Second.DataAccess.ApplicationDb;
using Second.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Second.DataAccess.Repositories
{
    public class JobRepository : IJobRepository 
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<IList<JobModel>> GetAllJobs() {
            return await _context.Jobs.Select(_=>new JobModel { JobId=_.JobId,JobTitle=_.JobTitle}).ToListAsync();
        } 
    }
}
