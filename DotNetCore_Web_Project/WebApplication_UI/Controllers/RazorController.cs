/*
 * Built-In HTML Helpers
  */
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using HelperClassLibrary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication_UI.Controllers
{
    public class RazorController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly RazorViewToStringRender _stringRender;
        private readonly IEnumerable<Student> _studentList = new List<Student>{
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 5, StudentName = "Rob" , Age = 19 }
            };

        public RazorController(IWebHostEnvironment env, RazorViewToStringRender stringRender)
        {
            _environment = env;
            _stringRender = stringRender;
        }

        public IActionResult RazorBase()
        {
            return View();
        }

        public IActionResult CustomHtmlHelperExtensions()
        {
            ViewData["columns"] = new string[] { "ID", "Name", "Price" };
            ViewData["content"] = new string[,]{
                {"101", "Apple", "1.01"},
                {"202", "Back", "2.02"},
                {"303", "Cup", "3.03"},
                {"404", "Donut", "3.03"}
            };
            return View();
        }

        public IActionResult BuildInHtmlhelper1()
        {
            var product = new Product(901, "Rocket", 99.99);
            return View(product);
        }

        public IActionResult BuildInHtmlhelper2()
        {
            var students = new List<Student>();
            students.Add(new Student { StudentId = 1, StudentGender = Gender.Male, SelectedTeaIds = new List<int>() { 1, 2, 3 } });
            students.Add(new Student { StudentId = 2, StudentGender = Gender.Female, SelectedTeaIds = new List<int>() { 1, 3 } });
            students.Add(new Student { StudentId = 3, StudentGender = Gender.Female, SelectedTeaIds = new List<int>() {  2, 3 } });

            ViewBag.TeaTypes = GetAllTeaTypes();

            return View(students);
        }

        public List<SelectListItem> GetAllTeaTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "General Tea", Value = "1" });
            items.Add(new SelectListItem { Text = "Coffee", Value = "2" });
            items.Add(new SelectListItem { Text = "Green Tea", Value = "3" });
            items.Add(new SelectListItem { Text = "Black Tea", Value = "4" });
            return items;
        }

        public IActionResult PartialViewAction()
        {
            var roles = new List<Transformer>();
            roles.Add(new Transformer(1, "Optimus Prime", "Earth", "Blue"));
            roles.Add(new Transformer(2, "Bumblebee", "Earth", "Yellow"));
            roles.Add(new Transformer(3, "Starscream", "Cybertron", "Red"));
            roles.Add(new Transformer(4, "Soundwave", "Cybertron", "Purple"));
            return View(roles);
        }
        [HttpGet]
        public PartialViewResult GetPartialViewResult()
        {
            var roles = new List<Transformer>();
            roles.Add(new Transformer(1, "Optimus Prime", "Earth", "Blue"));
            roles.Add(new Transformer(2, "Bumblebee", "Earth", "Yellow"));
            roles.Add(new Transformer(3, "Starscream", "Cybertron", "Red"));
            roles.Add(new Transformer(4, "Soundwave", "Cybertron", "Purple"));
            try
            {
                return PartialView("RowTableRowView", roles);
            }
            catch (Exception)
            {
                return PartialView();
            }
        }
        public IActionResult ActionLinkView()
        {
            return View(StaticDataSource.Countries);
        }

        public IActionResult VariableInHtml() {
            var stu = _studentList;

            return View(stu);
        }

        [HttpGet]
        public IActionResult UpdateNationalFlag(string code)
        {
            var country =
                StaticDataSource.Countries.SingleOrDefault(
                    c => c.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));
            return View(country);
        }

        [HttpPost]
        public IActionResult UpdateNationalFlag(string code, IFormFile nationalFlagFile)
        {
            if (nationalFlagFile == null || nationalFlagFile.Length == 0)
            {
                return RedirectToAction(nameof(ActionLinkView));
            }

            var targetFileName = $"{code}{Path.GetExtension(nationalFlagFile.FileName)}";
            var relativeFilePath = Path.Combine("Razor", targetFileName);
            var absoluteFilePath = Path.Combine(_environment.WebRootPath, relativeFilePath);
            var country =
                StaticDataSource.Countries.SingleOrDefault(
                    c => c.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));
            country.NationalFlagPath = targetFileName;
            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                nationalFlagFile.CopyTo(stream);
            }

            return RedirectToAction(nameof(ActionLinkView));
        }
        public IActionResult ViewLayoutRender() => View();

        [HttpGet("studentIndex")]
        public IActionResult StudentIndex()
        {
            return View(_studentList);
        }

        [HttpGet("test")]
        public Task<string> Test()
        {
            var students = _studentList;
            var html = _stringRender.RenderViewToStringAsync("Views/Razor/StudentIndex.cshtml", students);
            return html;
        }
    }
}