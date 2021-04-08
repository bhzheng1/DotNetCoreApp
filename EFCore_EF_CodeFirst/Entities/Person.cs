using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Entities
{
    [Table("person")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}