using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_API.ContosoEntities
{
    public class Enrollment : AuditableEntity
    {
        public Enrollment()
        {
            CreatedDate = DateTime.Now;
        }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }

    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }
}