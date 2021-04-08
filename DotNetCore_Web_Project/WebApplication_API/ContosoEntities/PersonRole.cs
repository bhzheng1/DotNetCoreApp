using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_API.ContosoEntities
{
    public class PersonRole
    {
        public int PersonId { get; set; }
        public int RoleId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
    }
}
