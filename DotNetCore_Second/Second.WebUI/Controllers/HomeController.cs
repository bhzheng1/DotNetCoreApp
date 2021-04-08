using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Second.DataAccess.Repositories;
using Second.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Second.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string xmlpath;
        private readonly IDepartmentRepository _departmentRepository;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            xmlpath = env.WebRootPath + @"\xml\xmlData.xml";
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReadXmlData()
        {
            var countries = new List<XmlCountryModel>();
            var doc = new XmlDocument();
            doc.Load(xmlpath);

            foreach (XmlNode _ in doc.SelectNodes("/xmlData/countryData/country"))
            {
                countries.Add(new XmlCountryModel
                {
                    Code = _.Attributes["code"].Value,
                    Name = _.InnerText,
                    Handle = _.Attributes["handle"].Value,
                    Continent = _.Attributes["continent"].Value,
                    Iso = Convert.ToInt32(_.Attributes["iso"].Value)
                });
            }
            var sortedCountries = countries.OrderBy(_=>_.Continent).ThenBy(_ => _.Iso).ToList();
            return View(sortedCountries);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public PartialViewResult GetModalSample() {
            return PartialView("_ModalSample");
        }

        public PartialViewResult GetDepartmentInfos()
        {
            var departments = _departmentRepository.GetDepartmentInfos();
            return PartialView("_ModalSampleWithTable", departments);
        }
        [HttpPost]
        public PartialViewResult GetDepartmentInfos(string hello)
        {
            ViewBag.Title = hello;
            var departments = _departmentRepository.GetDepartmentInfos();
            return PartialView("_ModalSampleWithTable2", departments);
        }
        public IActionResult Nothing() {
            return Content("Nothing");
        }

        public PartialViewResult ModalError() {
            return PartialView("_ModalError");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
