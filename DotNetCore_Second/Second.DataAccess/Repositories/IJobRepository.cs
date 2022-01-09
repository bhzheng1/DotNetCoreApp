using Second.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Second.DataAccess.Repositories
{
    public interface IJobRepository 
    {
        Task<IList<JobModel>> GetAllJobs();
    }
}
