using System.Collections.Generic;
using System.Threading.Tasks;
using Second.DataAccess.Entities;

namespace Second.DataAccess.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        public IList<Country> GetCountries();
    }
}