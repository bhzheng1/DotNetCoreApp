using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Entities
{
    [Table("Department")]
    public class Department
    {
        public Department()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}