using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_API.ContosoEntities
{
    public class Role : AuditableEntity
    {
        public Role()
        {
            CreatedDate = DateTime.Now;
        }

        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}