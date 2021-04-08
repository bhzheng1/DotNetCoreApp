using System.Collections.Generic;
using System.Threading.Tasks;
using One.Domain.Models;

namespace One.DAL.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryReadModel>> GetAllCountries();
    }
}