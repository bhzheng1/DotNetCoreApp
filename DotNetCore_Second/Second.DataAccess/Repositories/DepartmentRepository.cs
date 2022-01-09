using Second.DataAccess.ApplicationDb;
using Second.Model;
using System.Collections.Generic;
using System.Linq;

namespace Second.DataAccess.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DepartmentInfo> GetDepartmentInfos()
        {
            var query = from dept in _context.Departments
                        join loc in _context.Locations on dept.LocationId equals loc.LocationId
                        join con in _context.Countries on loc.CountryId equals con.CountryId
                        join reg in _context.Regions on con.RegionId equals reg.RegionId
                        select new DepartmentInfo()
                        {
                            DepartmentId = dept.DepartmentId,
                            DepartmentName = dept.DepartmentName,
                            LocationId = loc.LocationId,
                            StreetAddress = loc.StreetAddress,
                            PostalCode = loc.PostalCode,
                            City = loc.City,
                            StateProvince = loc.StateProvince,
                            CountryId = con.CountryId,
                            CountryName = con.CountryName,
                            RegionId = reg.RegionId,
                            RegionName = reg.RegionName
                        };
            return query.ToList();
        }
    }
}
