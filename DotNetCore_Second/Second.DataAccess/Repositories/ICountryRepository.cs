using Second.DataAccess.ApplicationDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Second.DataAccess.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<Country>> GetCountriesAsync();
        public IList<Country> GetCountries();
    }
}