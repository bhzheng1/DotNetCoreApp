using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DotNetCore.Entities
{
    [Table("user_role")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}