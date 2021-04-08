using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_API.SakilaModels;

namespace WebApplication_API.Repositories
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City> GetCityByIdAsync(Guid CityId);
        Task<City> GetCityWithDetailsAsync(Guid CityId);
        void CreateCity(City City);
        void UpdateCity(City City);
        void DeleteCity(City City);
    }
}
