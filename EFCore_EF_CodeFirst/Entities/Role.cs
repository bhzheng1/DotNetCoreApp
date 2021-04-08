using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Entities
{
    [Table("role")]
    public class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}