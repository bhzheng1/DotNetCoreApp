using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace DotNetCore.Entities
{
    [Table("user")]
    public class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}