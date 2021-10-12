using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.BasicAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var EmpList = new EmployeeBL().GetEmployees();
            switch (username.ToLower())
            {
                case "maleuser":
                    return EmpList.Where(e => e.Gender.ToLower() == "male").ToList();
                case "femaleuser":
                    return EmpList.Where(e => e.Gender.ToLower() == "female").ToList();
                default:
                    return await Task.FromResult<IEnumerable<Employee>>(null);
            }
        }
    }
}
