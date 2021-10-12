using Microsoft.AspNetCore.Mvc;
using Second.DataAccess.Repositories;

namespace Second.WebUI.Controllers
{
    public class TableController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public TableController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetDepartmentInfos();
            return View(departments);
        }
    }
}
