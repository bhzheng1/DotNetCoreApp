using Microsoft.AspNetCore.Mvc;
using Second.DataAccess.Repositories;
using System.Threading.Tasks;

namespace Second.WebUI.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<IActionResult> GetAllJobs() 
        { 
            return Json(await _jobRepository.GetAllJobs());
        }
    }
}
