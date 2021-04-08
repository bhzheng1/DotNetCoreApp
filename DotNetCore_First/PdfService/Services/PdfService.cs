using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeZoneNames;
using WebApplication1.Models;
using WebSupergoo.ABCpdf11;

namespace WebApplication1.Controllers
{
    public interface IPdfService
    {
        Task<Stream> GeneratePdf();
    }
    public class PdfService : IPdfService
    {
        private readonly IHostingEnvironment _env;
        private readonly RazorViewToStringRender _stringRender;
        private readonly IEnumerable<Student> _studentList = new List<Student>{
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                new Student() { StudentId = 4, StudentName = "Rob" , Age = 19 }
            };
        public PdfService(IHostingEnvironment env, RazorViewToStringRender stringRender)
        {
            _env = env;
            _stringRender = stringRender;
        }

        public Task<Stream> GeneratePdf()
        {
            Stream pdfStream;
            using (var theDoc = new Doc())
            {
                var students = _studentList;

                var logoPath = Path.Combine(_env.WebRootPath, "logo-white.png");
                var base64 = FileToBase64String(logoPath);


                var logo = new LogoSettings();
                //var logo = new LogoSettings() {ImageExtension = "png",LogoBase64String = base64};

                var docSettings = new DocSettings();
                var header = new HeaderSettings() { ReportTitle = "abc", ReportFor = "aaa", ReportAsOf = "bbb", ReportBasis = "ccc" };

                var html = _stringRender.RenderViewToStringAsync("Views/Student/Index.cshtml", students);

                LandscapePage(theDoc, docSettings);
                AddContent(theDoc, docSettings, html.Result);

                var theCount = theDoc.PageCount;
                for (var i = 0; i < theCount; i++)
                {
                    theDoc.PageNumber = i + 1;
                    AddHeader(theDoc, docSettings, header);
                    AddLogo(theDoc, docSettings, logo);
                    var foot = new FootSettings() { PageNumber = theDoc.PageNumber, TotalPage = theCount };
                    AddFooter(theDoc, docSettings, foot);
                    theDoc.Flatten();
                }

                pdfStream = theDoc.GetStream();
                //theDoc.Save(Path.Combine(_env.WebRootPath, "aaa.pdf"));
                theDoc.Clear();
            }

            //using (var theDoc = new Doc())
            //{
            //    theDoc.AddImageUrl(SvgBase64DataUrl());
            //    theDoc.Flatten();
            //    theDoc.Save(Path.Combine(_env.WebRootPath,"aab.pdf"));
            //    theDoc.Clear();
            //}
            return Task.FromResult(pdfStream);
        }
        //1. svg, html file can be read to html string

        private string FileToHtmlString(string filePath)
        {
            var htmlPath = filePath ?? Path.Combine(_env.WebRootPath, "a.html");
            return System.IO.File.ReadAllText(htmlPath);
        }

        //2. html or svg base64 data was decoded back to html string
        private string Base64ToString(string base64)
        {
            string text;
            var bytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(bytes))
            {
                var sr = new StreamReader(ms);
                text = sr.ReadToEnd();
            }
            //return System.Text.Encoding.ASCII.GetString(bytes);
            return text;
        }

        //3 svg base64 Data Url Can directly add to doc.AddImageUrl
        private string SvgBase64DataUrl(string dataUrl = null)
        {
            return dataUrl ?? @"data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyNzAiIGhlaWdodD0iNTAiPjxyZWN0IGlkPSJiYWNrZ3JvdW5kcmVjdCIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgeD0iMCIgeT0iMCIgZmlsbD0ibm9uZSIgc3Ryb2tlPSJub25lIi8+PGcgY2xhc3M9ImN1cnJlbnRMYXllciIgc3R5bGU9IiI+PHRpdGxlPkxheWVyIDE8L3RpdGxlPjxwYXRoIGQ9Ik04NS43IDQ5LjFWLjhoNi4ydjQ4LjNoLTYuMnpNNzQuOSA2LjRWLjhINDMuOHY0OC4zaDMxLjF2LTUuNmgtMjVWMjcuMmgyNC41di01LjZINDkuOVY2LjRoMjVNMCA0Mi4ybDMuNy00LjlhMTkuMiAxOS4yIDAgMCAwIDE0LjcgN2M4LjIgMCAxMC45LTQuNSAxMC45LTguMSAwLTEyLjQtMjcuOC01LjMtMjcuOC0yMi43IDAtOCA3LTEzLjUgMTYuNC0xMy41QzI1IDAgMzAuNiAyLjUgMzQuNyA2LjlMMzEgMTEuNmMtMy42LTQuMS04LjQtNS44LTEzLjYtNS44cy05LjUgMy05LjUgNy4zYzAgMTAuOCAyNy44IDQuNCAyNy44IDIyLjcgMCA2LjctNC42IDE0LjItMTcuNSAxNC4yLTguMSAwLTE0LjMtMy4yLTE4LjItNy44ek0xMjUuMiAxOC41bC0xMC0xNHYxNGgtMi4xVjFoMi4ybDkuOSAxMy43VjFoMi4xdjE3LjVoLTIuMXpNMTI5LjcgMTIuMWMwLTMuNyAyLjYtNi43IDYuMS02LjdzNiAzIDYgNi44di41aC0xMGE0LjQgNC40IDAgMCAwIDQuNCA0LjQgNS4zIDUuMyAwIDAgMCAzLjktMS42bC45IDEuM2E2LjggNi44IDAgMCAxLTQuOSAyYy0zLjcgMC02LjQtMi43LTYuNC02Ljd6bTYuMS01YTQuMiA0LjIgMCAwIDAtNC4xIDQuMmg4LjFhNC4xIDQuMSAwIDAgMC00LTQuMnpNMTU1LjEgMTguNWwtMy4yLTEwLjItMy4yIDEwLjJoLTEuOWwtNC0xMi43aDJsMyAxMC4xTDE1MSA1LjhoMS43bDMuMiAxMC4xIDMtMTAuMWgybC00IDEyLjdoLTJ6TTE4MSAxOC41bC0zLjItMTAuMi0zLjIgMTAuMmgtMS45bC00LTEyLjdoMmwzIDEwLjFMMTc3IDUuOGgxLjdsMy4yIDEwLjEgMy0xMC4xaDJsLTQgMTIuN2gtMnpNMTk2LjYgMTguNVYxN2E1LjMgNS4zIDAgMCAxLTQuMSAxLjggNC4yIDQuMiAwIDAgMS00LjMtNC4yIDQuMSA0LjEgMCAwIDEgNC4zLTQuMiA1LjIgNS4yIDAgMCAxIDQuMSAxLjdWOS44YzAtMS43LTEuMy0yLjctMy4xLTIuN2E0LjggNC44IDAgMCAwLTMuOCAxLjlsLS45LTEuNGE2LjQgNi40IDAgMCAxIDUtMi4xYzIuNiAwIDQuOCAxLjIgNC44IDQuM3Y4LjdoLTEuOXptMC0yLjd2LTIuNGE0LjIgNC4yIDAgMCAwLTMuNC0xLjYgMi44IDIuOCAwIDEgMCAwIDUuNSA0LjEgNC4xIDAgMCAwIDMuNC0xLjV6TTIwMS44IDIxLjdsMS4xLjJhMS44IDEuOCAwIDAgMCAxLjktMS4zbC44LTEuOS01LjItMTIuOWgyLjFsNC4xIDEwLjQgNC4xLTEwLjRoMi4xbC02LjIgMTUuM2EzLjcgMy43IDAgMCAxLTMuNiAyLjZsLTEuNC0uMnpNMjEzLjcgMTYuOGwxLTEuNGE1LjYgNS42IDAgMCAwIDQuMSAxLjhjMS45IDAgMy0uOSAzLTIuMiAwLTMuMS03LjctMS4yLTcuNy01LjkgMC0yIDEuNi0zLjcgNC42LTMuN2E2LjEgNi4xIDAgMCAxIDQuNiAxLjhsLS45IDEuNGE0LjcgNC43IDAgMCAwLTMuNy0xLjZjLTEuNyAwLTIuOC45LTIuOCAyIDAgMi44IDcuNy45IDcuNyA1LjkgMCAyLjEtMS43IDMuOC00LjkgMy44YTYuNiA2LjYgMCAwIDEtNS0xLjl6TTIyNi4xIDE3LjNhMS40IDEuNCAwIDEgMSAxLjQgMS40IDEuNCAxLjQgMCAwIDEtMS40LTEuNHpNMTI1LjIgNDkuMmwtMTAtMTR2MTRoLTIuMVYzMS42aDIuMmw5LjkgMTMuN1YzMS42aDIuMXYxNy42aC0yLjF6TTEyOS43IDQyLjhjMC0zLjcgMi42LTYuNyA2LjEtNi43czYgMyA2IDYuOHYuNWgtMTBhNC40IDQuNCAwIDAgMCA0LjQgNC40IDUuMyA1LjMgMCAwIDAgMy45LTEuNmwuOSAxLjNhNi44IDYuOCAwIDAgMS00LjkgMmMtMy43IDAtNi40LTIuNy02LjQtNi43em02LjEtNWE0LjIgNC4yIDAgMCAwLTQuMSA0LjJoOC4xYTQuMSA0LjEgMCAwIDAtNC00LjJ6TTE1NS4xIDQ5LjJMMTUxLjkgMzlsLTMuMiAxMC4yaC0xLjlsLTQtMTIuN2gybDMgMTAuMSAzLjItMTAuMWgxLjdsMy4yIDEwLjEgMy0xMC4xaDJsLTQgMTIuN2gtMnpNMTc3LjYgNDkuMnYtMS41YTUuMyA1LjMgMCAwIDEtNC4xIDEuOCA0LjIgNC4yIDAgMCAxLTQuMy00LjIgNC4xIDQuMSAwIDAgMSA0LjMtNC4yIDUuMiA1LjIgMCAwIDEgNC4xIDEuN3YtMi4zYzAtMS43LTEuMy0yLjctMy4xLTIuN2E0LjggNC44IDAgMCAwLTMuOCAxLjhsLS45LTEuNGE2LjQgNi40IDAgMCAxIDUtMi4xYzIuNiAwIDQuOCAxLjIgNC44IDQuM3Y4LjdoLTEuOXptMC0yLjd2LTIuNGE0LjIgNC4yIDAgMCAwLTMuNC0xLjYgMi44IDIuOCAwIDEgMCAwIDUuNSA0LjEgNC4xIDAgMCAwIDMuNC0xLjV6TTE5MSA0OS4ydi04LjNjMC0yLjMtMS4xLTIuOS0yLjgtMi45YTQuOCA0LjggMCAwIDAtMy43IDJ2OS4zaC0xLjlWMzYuNWgxLjl2MS44YTYgNiAwIDAgMSA0LjQtMi4yYzIuNiAwIDMuOSAxLjMgMy45IDQuMXY4LjlIMTkxek0xOTUuNSA0Ny41bDEtMS40YTUuNiA1LjYgMCAwIDAgNC4xIDEuOGMxLjkgMCAzLS45IDMtMi4yIDAtMy4xLTcuNy0xLjItNy43LTUuOSAwLTIgMS42LTMuNyA0LjYtMy43QTYuMSA2LjEgMCAwIDEgMjA1IDM4bC0uOSAxLjRhNC43IDQuNyAwIDAgMC0zLjctMS42Yy0xLjcgMC0yLjguOS0yLjggMiAwIDIuOCA3LjcuOSA3LjcgNS45IDAgMi4xLTEuNyAzLjgtNC45IDMuOGE2LjYgNi42IDAgMCAxLTQuOS0yek0yMTkuNCA0OS4yTDIxNi4yIDM5IDIxMyA0OS4yaC0ybC00LTEyLjdoMmwzIDEwLjEgMy4zLTEwLjFoMS43bDMuMiAxMC4xIDMtMTAuMWgybC00IDEyLjdoLTJ6TTIyNi42IDQyLjhjMC0zLjcgMi42LTYuNyA2LjEtNi43czYgMyA2IDYuOHYuNWgtMTBhNC40IDQuNCAwIDAgMCA0LjQgNC40IDUuMyA1LjMgMCAwIDAgMy45LTEuNmwuOSAxLjNhNi44IDYuOCAwIDAgMS00LjkgMmMtMy44IDAtNi40LTIuNy02LjQtNi43em02LjEtNWE0LjIgNC4yIDAgMCAwLTQuMSA0LjJoOC4xYTQuMSA0LjEgMCAwIDAtNC00LjJ6TTI0MS41IDQ5LjJWMzYuNWgxLjl2MmE1LjMgNS4zIDAgMCAxIDQuMS0yLjN2MmgtLjdhNC41IDQuNSAwIDAgMC0zLjQgMnY5aC0xLjl6TTI0OS4yIDQ3LjVsMS0xLjRhNS42IDUuNiAwIDAgMCA0LjEgMS44YzEuOSAwIDMtLjkgMy0yLjIgMC0zLjEtNy43LTEuMi03LjctNS45IDAtMiAxLjYtMy43IDQuNi0zLjdhNi4xIDYuMSAwIDAgMSA0LjYgMS44bC0uOSAxLjRhNC43IDQuNyAwIDAgMC0zLjctMS42Yy0xLjcgMC0yLjguOS0yLjggMiAwIDIuOCA3LjcuOSA3LjcgNS45IDAgMi4xLTEuNyAzLjgtNC45IDMuOGE2LjYgNi42IDAgMCAxLTUtMS45ek0yNjEuNSA0OGExLjQgMS40IDAgMSAxIDEuNCAxLjQgMS40IDEuNCAwIDAgMS0xLjQtMS40ek0yNjYuNyAzOC43YTMuNCAzLjQgMCAxIDEgMy4zLTMuNCAzLjMgMy4zIDAgMCAxLTMuMyAzLjR6bTAtNi4yYTIuOCAyLjggMCAxIDAgMi44IDIuOCAyLjggMi44IDAgMCAwLTIuOC0yLjd6bS45IDQuOGwtMS0xLjVoLS42djEuNWgtLjZ2LTMuOWgxLjZhMS4yIDEuMiAwIDAgMSAxLjMgMS4yIDEuMSAxLjEgMCAwIDEtMSAxLjFsMSAxLjVoLS43em0tLjYtMy40aC0xdjEuM2gxYS43LjcgMCAwIDAgLjctLjcuNy43IDAgMCAwLS43LS42eiI+PC9wYXRoPjwvZz48L3N2Zz4=";
        }
        public Image Base64StringToImage(string base64)
        {
            Image image;
            var bytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        private string FileToBase64String(string filePath)
        {
            var ms = new MemoryStream();

            using (var s = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                s.CopyTo(ms);
                ms.Position = 0;
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        private Doc LandscapePage(Doc theDoc, DocSettings docSettings)
        {
            theDoc.MediaBox.String = $"0 0 {docSettings.DocWidth} {docSettings.DocHeight}";
            theDoc.SetInfo(theDoc.Page, "/MediaBox:Rect", $"0 0 {docSettings.DocWidth} {docSettings.DocHeight}");
            theDoc.TopDown = true;

            return theDoc;
        }

        private Doc AddLogo(Doc theDoc, DocSettings docSettings, LogoSettings logo)
        {
            var originalWidth = 270;
            var originalHeight = 50;
            var scaleRatio = 0.6;
            theDoc.TopDown = true;
            theDoc.Rect.Pin = XRect.Corner.TopLeft;
            theDoc.Rect.Position(0, 0);
            theDoc.Rect.Resize(originalWidth * scaleRatio, originalHeight * scaleRatio);
            theDoc.Rect.Move(docSettings.LeftMargin + 5, docSettings.TopMargin + 5);

            var data = Convert.FromBase64String(logo.LogoBase64String);
            if (logo.ImageExtension.ToLower() == "svg")
            {
                var svg = System.Text.Encoding.ASCII.GetString(data);
                theDoc.AddImageHtml(svg, false, 360, false);
            }
            else
            {
                var image = XImage.FromData(data, new XReadOptions());
                theDoc.AddImageObject(image, true);
            }

            return theDoc;
        }

        private Doc AddHeader(Doc theDoc, DocSettings docSettings, HeaderSettings header)
        {
            theDoc.TopDown = true;
            theDoc.Rect.Pin = XRect.Corner.TopLeft;
            theDoc.Rect.Position(0, 0);

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 40);
            theDoc.Rect.Move(docSettings.LeftMargin, docSettings.TopMargin);
            theDoc.Color.Color = ColorTranslator.FromHtml(header.BackBarColor);
            theDoc.FillRect();

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 37);
            theDoc.Rect.Move(0, 0);
            theDoc.Color.Color = ColorTranslator.FromHtml(header.FrontBarColor);
            theDoc.FillRect();

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 37);
            theDoc.Rect.Position(docSettings.LeftMargin, docSettings.TopMargin);
            theDoc.TextStyle.HPos = 0.5;
            theDoc.TextStyle.VPos = 0.5;
            theDoc.Color.Color = ColorTranslator.FromHtml(header.HeaderTitleColor);
            theDoc.FontSize = 14;
            theDoc.AddTextStyled("<b>" + header.ReportTitle + "</b>");

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 20);
            theDoc.Rect.Move(0, 40);
            theDoc.TextStyle.HPos = 0.001;
            theDoc.TextStyle.VPos = 1;
            theDoc.Color.Color = ColorTranslator.FromHtml("#000000");
            theDoc.FontSize = 12;
            theDoc.AddTextStyled("<b>For:</b> " + header.ReportFor);

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 20);
            theDoc.Rect.Move(0, 0);
            theDoc.TextStyle.HPos = 0.6;
            theDoc.TextStyle.VPos = 1;
            theDoc.Color.Color = ColorTranslator.FromHtml("#000000");
            theDoc.FontSize = 12;
            theDoc.AddTextStyled("<b>As Of:</b> " + header.ReportAsOf);

            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 20);
            theDoc.Rect.Move(0, 0);
            theDoc.TextStyle.HPos = 0.999;
            theDoc.TextStyle.VPos = 1;
            theDoc.Color.Color = ColorTranslator.FromHtml("#000000");
            theDoc.FontSize = 12;
            theDoc.AddTextStyled("<b>Report Basis:</b> " + header.ReportBasis);

            theDoc.Width = 0.8;
            theDoc.AddLine(docSettings.LeftMargin, docSettings.TopMargin + 60, docSettings.DocWidth - docSettings.RightMargin, docSettings.TopMargin + 60);

            return theDoc;
        }
        private Doc AddFooter(Doc theDoc, DocSettings docSettings, FootSettings foot)
        {
            theDoc.TopDown = true;
            theDoc.Color.Color = ColorTranslator.FromHtml("#000000");
            theDoc.Width = 1;
            theDoc.AddLine(docSettings.LeftMargin, docSettings.DocHeight - docSettings.BottomMargin - 30, docSettings.DocWidth - docSettings.RightMargin, docSettings.DocHeight - docSettings.BottomMargin - 30);

            theDoc.Rect.Pin = XRect.Corner.TopLeft;
            theDoc.Rect.Position(docSettings.LeftMargin, docSettings.DocHeight - docSettings.BottomMargin - 30);
            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 30);
            theDoc.TextStyle.HPos = 0;
            theDoc.TextStyle.VPos = 0.3;
            var originalLineSpacing = theDoc.TextStyle.LineSpacing;
            theDoc.TextStyle.LineSpacing = 1.5;
            var text1 = "<b>Report ID:</b> " + foot.ReportId + "<BR><b>Created:</b> " + foot.CreatedDayTime;
            theDoc.Color.Color = ColorTranslator.FromHtml("#000000");
            theDoc.FontSize = 8;
            theDoc.AddTextStyled(text1);
            theDoc.TextStyle.LineSpacing = originalLineSpacing;

            if (foot.PageNumber == 0 || foot.TotalPage == 0) return theDoc;
            theDoc.Rect.Position(docSettings.LeftMargin, docSettings.DocHeight - docSettings.BottomMargin - 30);
            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, 30);
            theDoc.TextStyle.HPos = 0.5;
            theDoc.TextStyle.VPos = 0.5;
            var text2 = "Page " + foot.PageNumber + " of " + foot.TotalPage;
            theDoc.FontSize = 8;
            theDoc.AddTextStyled(text2);
            return theDoc;
        }

        private Doc AddContent(Doc theDoc, DocSettings docSettings, string html)
        {
            theDoc.TopDown = true;
            theDoc.Rect.Pin = XRect.Corner.TopLeft;
            theDoc.Rect.Position(docSettings.LeftMargin, docSettings.TopMargin + 60);
            theDoc.Rect.Resize(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin, docSettings.DocHeight - docSettings.TopMargin - docSettings.BottomMargin - 90);
            //theDoc.Rect.Inset(0, 3);
            theDoc.TextStyle.HPos = 0.5;
            theDoc.HtmlOptions.Engine = EngineType.Chrome;
            theDoc.HtmlOptions.UseScript = true;
            theDoc.HtmlOptions.UseActiveX = true;
            theDoc.HtmlOptions.UseVideo = true;
            theDoc.HtmlOptions.Timeout = 120000;
            theDoc.HtmlOptions.PageCacheEnabled = true;
            theDoc.HtmlOptions.Media = MediaType.Print;
            theDoc.HtmlOptions.InitialWidth = (int)(docSettings.DocWidth - docSettings.LeftMargin - docSettings.RightMargin);

            var theId = theDoc.AddImageHtml(html);
            while (true)
            {
                if (!theDoc.Chainable(theId))
                    break;
                theDoc.Page = theDoc.AddPage();
                theDoc.Rendering.DotsPerInch = docSettings.DotPerInches;
                theDoc.Rendering.SaveQuality = docSettings.SaveQuality;

                theId = theDoc.AddImageToChain(theId);
            }
            return theDoc;
        }
    }
    public class DocSettings
    {
        public DocSettings()
        {
            DocWidth = 792;
            DocHeight = 612;
            LeftMargin = 24;
            RightMargin = 24;
            TopMargin = 25;
            BottomMargin = 15;
        }
        public double DocWidth { get; set; }
        public double DocHeight { get; set; }
        public double LeftMargin { get; set; }
        public double RightMargin { get; set; }
        public double TopMargin { get; set; }
        public double BottomMargin { get; set; }
        public double DotPerInches { get; set; }
        public int SaveQuality { get; set; }
    }
    public class FootSettings
    {
        public FootSettings()
        {
            CreatedDayTime = GetDayTime();
        }
        public string ReportId { get; set; }
        public string CreatedDayTime { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }

        public string GetDayTime()
        {
            var timeZoneId = TimeZoneInfo.Local.Id;
            var timeZoneAbb = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now)
                ? TZNames.GetAbbreviationsForTimeZone(timeZoneId, "en-US").Daylight
                : TZNames.GetAbbreviationsForTimeZone(timeZoneId, "en-US").Standard;

            var now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var k = DateTime.Now.ToString("'(GMT' K')'");
            return now + " " + timeZoneAbb + " " + " " + k;
        }
    }

    public class LogoSettings
    {
        public LogoSettings()
        {
            LogoBase64String = "PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyNzAiIGhlaWdodD0iNTAiPjxyZWN0IGlkPSJiYWNrZ3JvdW5kcmVjdCIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgeD0iMCIgeT0iMCIgZmlsbD0ibm9uZSIgc3Ryb2tlPSJub25lIi8+PGcgY2xhc3M9ImN1cnJlbnRMYXllciIgc3R5bGU9IiI+PHRpdGxlPkxheWVyIDE8L3RpdGxlPjxwYXRoIGQ9Ik04NS43IDQ5LjFWLjhoNi4ydjQ4LjNoLTYuMnpNNzQuOSA2LjRWLjhINDMuOHY0OC4zaDMxLjF2LTUuNmgtMjVWMjcuMmgyNC41di01LjZINDkuOVY2LjRoMjVNMCA0Mi4ybDMuNy00LjlhMTkuMiAxOS4yIDAgMCAwIDE0LjcgN2M4LjIgMCAxMC45LTQuNSAxMC45LTguMSAwLTEyLjQtMjcuOC01LjMtMjcuOC0yMi43IDAtOCA3LTEzLjUgMTYuNC0xMy41QzI1IDAgMzAuNiAyLjUgMzQuNyA2LjlMMzEgMTEuNmMtMy42LTQuMS04LjQtNS44LTEzLjYtNS44cy05LjUgMy05LjUgNy4zYzAgMTAuOCAyNy44IDQuNCAyNy44IDIyLjcgMCA2LjctNC42IDE0LjItMTcuNSAxNC4yLTguMSAwLTE0LjMtMy4yLTE4LjItNy44ek0xMjUuMiAxOC41bC0xMC0xNHYxNGgtMi4xVjFoMi4ybDkuOSAxMy43VjFoMi4xdjE3LjVoLTIuMXpNMTI5LjcgMTIuMWMwLTMuNyAyLjYtNi43IDYuMS02LjdzNiAzIDYgNi44di41aC0xMGE0LjQgNC40IDAgMCAwIDQuNCA0LjQgNS4zIDUuMyAwIDAgMCAzLjktMS42bC45IDEuM2E2LjggNi44IDAgMCAxLTQuOSAyYy0zLjcgMC02LjQtMi43LTYuNC02Ljd6bTYuMS01YTQuMiA0LjIgMCAwIDAtNC4xIDQuMmg4LjFhNC4xIDQuMSAwIDAgMC00LTQuMnpNMTU1LjEgMTguNWwtMy4yLTEwLjItMy4yIDEwLjJoLTEuOWwtNC0xMi43aDJsMyAxMC4xTDE1MSA1LjhoMS43bDMuMiAxMC4xIDMtMTAuMWgybC00IDEyLjdoLTJ6TTE4MSAxOC41bC0zLjItMTAuMi0zLjIgMTAuMmgtMS45bC00LTEyLjdoMmwzIDEwLjFMMTc3IDUuOGgxLjdsMy4yIDEwLjEgMy0xMC4xaDJsLTQgMTIuN2gtMnpNMTk2LjYgMTguNVYxN2E1LjMgNS4zIDAgMCAxLTQuMSAxLjggNC4yIDQuMiAwIDAgMS00LjMtNC4yIDQuMSA0LjEgMCAwIDEgNC4zLTQuMiA1LjIgNS4yIDAgMCAxIDQuMSAxLjdWOS44YzAtMS43LTEuMy0yLjctMy4xLTIuN2E0LjggNC44IDAgMCAwLTMuOCAxLjlsLS45LTEuNGE2LjQgNi40IDAgMCAxIDUtMi4xYzIuNiAwIDQuOCAxLjIgNC44IDQuM3Y4LjdoLTEuOXptMC0yLjd2LTIuNGE0LjIgNC4yIDAgMCAwLTMuNC0xLjYgMi44IDIuOCAwIDEgMCAwIDUuNSA0LjEgNC4xIDAgMCAwIDMuNC0xLjV6TTIwMS44IDIxLjdsMS4xLjJhMS44IDEuOCAwIDAgMCAxLjktMS4zbC44LTEuOS01LjItMTIuOWgyLjFsNC4xIDEwLjQgNC4xLTEwLjRoMi4xbC02LjIgMTUuM2EzLjcgMy43IDAgMCAxLTMuNiAyLjZsLTEuNC0uMnpNMjEzLjcgMTYuOGwxLTEuNGE1LjYgNS42IDAgMCAwIDQuMSAxLjhjMS45IDAgMy0uOSAzLTIuMiAwLTMuMS03LjctMS4yLTcuNy01LjkgMC0yIDEuNi0zLjcgNC42LTMuN2E2LjEgNi4xIDAgMCAxIDQuNiAxLjhsLS45IDEuNGE0LjcgNC43IDAgMCAwLTMuNy0xLjZjLTEuNyAwLTIuOC45LTIuOCAyIDAgMi44IDcuNy45IDcuNyA1LjkgMCAyLjEtMS43IDMuOC00LjkgMy44YTYuNiA2LjYgMCAwIDEtNS0xLjl6TTIyNi4xIDE3LjNhMS40IDEuNCAwIDEgMSAxLjQgMS40IDEuNCAxLjQgMCAwIDEtMS40LTEuNHpNMTI1LjIgNDkuMmwtMTAtMTR2MTRoLTIuMVYzMS42aDIuMmw5LjkgMTMuN1YzMS42aDIuMXYxNy42aC0yLjF6TTEyOS43IDQyLjhjMC0zLjcgMi42LTYuNyA2LjEtNi43czYgMyA2IDYuOHYuNWgtMTBhNC40IDQuNCAwIDAgMCA0LjQgNC40IDUuMyA1LjMgMCAwIDAgMy45LTEuNmwuOSAxLjNhNi44IDYuOCAwIDAgMS00LjkgMmMtMy43IDAtNi40LTIuNy02LjQtNi43em02LjEtNWE0LjIgNC4yIDAgMCAwLTQuMSA0LjJoOC4xYTQuMSA0LjEgMCAwIDAtNC00LjJ6TTE1NS4xIDQ5LjJMMTUxLjkgMzlsLTMuMiAxMC4yaC0xLjlsLTQtMTIuN2gybDMgMTAuMSAzLjItMTAuMWgxLjdsMy4yIDEwLjEgMy0xMC4xaDJsLTQgMTIuN2gtMnpNMTc3LjYgNDkuMnYtMS41YTUuMyA1LjMgMCAwIDEtNC4xIDEuOCA0LjIgNC4yIDAgMCAxLTQuMy00LjIgNC4xIDQuMSAwIDAgMSA0LjMtNC4yIDUuMiA1LjIgMCAwIDEgNC4xIDEuN3YtMi4zYzAtMS43LTEuMy0yLjctMy4xLTIuN2E0LjggNC44IDAgMCAwLTMuOCAxLjhsLS45LTEuNGE2LjQgNi40IDAgMCAxIDUtMi4xYzIuNiAwIDQuOCAxLjIgNC44IDQuM3Y4LjdoLTEuOXptMC0yLjd2LTIuNGE0LjIgNC4yIDAgMCAwLTMuNC0xLjYgMi44IDIuOCAwIDEgMCAwIDUuNSA0LjEgNC4xIDAgMCAwIDMuNC0xLjV6TTE5MSA0OS4ydi04LjNjMC0yLjMtMS4xLTIuOS0yLjgtMi45YTQuOCA0LjggMCAwIDAtMy43IDJ2OS4zaC0xLjlWMzYuNWgxLjl2MS44YTYgNiAwIDAgMSA0LjQtMi4yYzIuNiAwIDMuOSAxLjMgMy45IDQuMXY4LjlIMTkxek0xOTUuNSA0Ny41bDEtMS40YTUuNiA1LjYgMCAwIDAgNC4xIDEuOGMxLjkgMCAzLS45IDMtMi4yIDAtMy4xLTcuNy0xLjItNy43LTUuOSAwLTIgMS42LTMuNyA0LjYtMy43QTYuMSA2LjEgMCAwIDEgMjA1IDM4bC0uOSAxLjRhNC43IDQuNyAwIDAgMC0zLjctMS42Yy0xLjcgMC0yLjguOS0yLjggMiAwIDIuOCA3LjcuOSA3LjcgNS45IDAgMi4xLTEuNyAzLjgtNC45IDMuOGE2LjYgNi42IDAgMCAxLTQuOS0yek0yMTkuNCA0OS4yTDIxNi4yIDM5IDIxMyA0OS4yaC0ybC00LTEyLjdoMmwzIDEwLjEgMy4zLTEwLjFoMS43bDMuMiAxMC4xIDMtMTAuMWgybC00IDEyLjdoLTJ6TTIyNi42IDQyLjhjMC0zLjcgMi42LTYuNyA2LjEtNi43czYgMyA2IDYuOHYuNWgtMTBhNC40IDQuNCAwIDAgMCA0LjQgNC40IDUuMyA1LjMgMCAwIDAgMy45LTEuNmwuOSAxLjNhNi44IDYuOCAwIDAgMS00LjkgMmMtMy44IDAtNi40LTIuNy02LjQtNi43em02LjEtNWE0LjIgNC4yIDAgMCAwLTQuMSA0LjJoOC4xYTQuMSA0LjEgMCAwIDAtNC00LjJ6TTI0MS41IDQ5LjJWMzYuNWgxLjl2MmE1LjMgNS4zIDAgMCAxIDQuMS0yLjN2MmgtLjdhNC41IDQuNSAwIDAgMC0zLjQgMnY5aC0xLjl6TTI0OS4yIDQ3LjVsMS0xLjRhNS42IDUuNiAwIDAgMCA0LjEgMS44YzEuOSAwIDMtLjkgMy0yLjIgMC0zLjEtNy43LTEuMi03LjctNS45IDAtMiAxLjYtMy43IDQuNi0zLjdhNi4xIDYuMSAwIDAgMSA0LjYgMS44bC0uOSAxLjRhNC43IDQuNyAwIDAgMC0zLjctMS42Yy0xLjcgMC0yLjguOS0yLjggMiAwIDIuOCA3LjcuOSA3LjcgNS45IDAgMi4xLTEuNyAzLjgtNC45IDMuOGE2LjYgNi42IDAgMCAxLTUtMS45ek0yNjEuNSA0OGExLjQgMS40IDAgMSAxIDEuNCAxLjQgMS40IDEuNCAwIDAgMS0xLjQtMS40ek0yNjYuNyAzOC43YTMuNCAzLjQgMCAxIDEgMy4zLTMuNCAzLjMgMy4zIDAgMCAxLTMuMyAzLjR6bTAtNi4yYTIuOCAyLjggMCAxIDAgMi44IDIuOCAyLjggMi44IDAgMCAwLTIuOC0yLjd6bS45IDQuOGwtMS0xLjVoLS42djEuNWgtLjZ2LTMuOWgxLjZhMS4yIDEuMiAwIDAgMSAxLjMgMS4yIDEuMSAxLjEgMCAwIDEtMSAxLjFsMSAxLjVoLS43em0tLjYtMy40aC0xdjEuM2gxYS43LjcgMCAwIDAgLjctLjcuNy43IDAgMCAwLS43LS42eiIgaWQ9InN2Z18xIiBjbGFzcz0iIiBmaWxsPSIjZmZmZmZmIiBmaWxsLW9wYWNpdHk9IjEiLz48L2c+PC9zdmc+";
            ImageExtension = "svg";
        }
        public string LogoBase64String { get; set; }
        public string ImageExtension { get; set; }
    }

    public class HeaderSettings
    {
        public HeaderSettings()
        {
            FrontBarColor = "#304048";
            BackBarColor = "#F37921";
            HeaderTitleColor = "#FFFFFF";
        }
        public string ReportTitle { get; set; }
        public string ReportFor { get; set; }
        public string ReportAsOf { get; set; }
        public string ReportBasis { get; set; }
        public string FrontBarColor { get; set; }
        public string BackBarColor { get; set; }
        public string HeaderTitleColor { get; set; }
    }

    public static class StringExtension
    {
        public static bool IsValidFormatHtmlColor(this string inputColor)
        {
            //regex from http://stackoverflow.com/a/1636354/2343
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                return true;

            var result = Color.FromName(inputColor);
            return result.IsKnownColor;
        }
    }
}
