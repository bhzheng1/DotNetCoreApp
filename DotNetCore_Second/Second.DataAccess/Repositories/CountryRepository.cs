using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second.DataAccess.Entities;

namespace Second.DataAccess.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly ApplicationDbContext _context;
        public CountryRepository(RepositoryContext repositoryContext, ApplicationDbContext context)
        {
            _repositoryContext = repositoryContext;
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var sb = new StringBuilder();
            sb.Append(@"select [country_id] as CountryId, [country_name] as CountryName, [region_id] as RegionId from hr.countries");
            var cd = await _repositoryContext.GetCommandDefinition(sb.ToString());
            return await _repositoryContext.QueryAsync<Country>(cd);
        }

        public IList<Country> GetCountries()
        {
            var result = _context.Countries.ToList();
            return result;
        }



    }
}
