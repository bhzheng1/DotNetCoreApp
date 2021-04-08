using Second.Model;
using System.Collections.Generic;

namespace Second.DataAccess.Repositories
{
    public interface IDepartmentRepository
    {
        public IList<DepartmentInfo> GetDepartmentInfos();
    }
}
