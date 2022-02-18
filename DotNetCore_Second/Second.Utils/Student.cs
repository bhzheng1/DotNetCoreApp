using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Second.Utils
{
    public class Student
    {
        public static IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", Age = 13, StandardID =1, Graduated = true },
            new Student() { StudentID = 2, StudentName = "Moin",  Age = 21, StandardID =1, Graduated = true},
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID =2, Graduated = false },
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID =2, Graduated = false},
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 15,StandardID = 3, Graduated = false}
            };
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int StandardID { get; set; }
        public int Age { get; set; }
        public bool Graduated { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Conference Date")]
        public DateTime? ResearchApplicationConferenceDate { get; set; }
    }
}
