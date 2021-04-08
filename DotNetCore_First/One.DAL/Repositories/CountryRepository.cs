using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using One.Domain.Models;

namespace One.DAL.Repositories
{
    public class CountryRepository:ICountryRepository
    {
        private readonly RepositoryContext _repositoryContext;
        public CountryRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<CountryReadModel>> GetAllCountries()
        {
            var sb = new StringBuilder();
            sb.Append(@"select [country_id] as CountryId, [country_name] as CountryName, [region_id] as RegionId from hr.countries");
            var cd = await _repositoryContext.GetCommandDefinition(sb.ToString());
            return await _repositoryContext.QueryAsync<CountryReadModel>(cd);
        }
    }
}
