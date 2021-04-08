using Microsoft.AspNetCore.Mvc;
using Second.DataAccess.Repositories;

namespace Second.WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetDepartmentInfos();
            return View(departments);
        }

        public IActionResult Edit()
        {
            var departments = _departmentRepository.GetDepartmentInfos();
            return View(departments);
        }
    }
}
