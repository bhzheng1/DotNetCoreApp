using System.Collections.Generic;

namespace ModelClassLibrary
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public Gender StudentGender { get; set; }
        public IList<int> SelectedTeaIds { get; set; }
    }
}
