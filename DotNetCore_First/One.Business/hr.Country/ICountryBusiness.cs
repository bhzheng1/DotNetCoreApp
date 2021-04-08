using System.Collections.Generic;
using System.Threading.Tasks;
using One.Domain.Models;
using One.Domain.ServiceRequest;
using One.Domain.ServiceResponse;

namespace One.Business.hr.Country
{
    public interface ICountryBusiness
    {
        Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAll(CountryRequest request);
    }
}