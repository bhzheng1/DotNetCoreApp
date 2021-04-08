using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_API.ContosoEntities
{
    [Table("Student")]
    public class Student : Person
    {
        public Student()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}