using ModelClassLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication_API.Repositories
{
    public interface IAddressRepository
    {
        Task<AddressModel> GetAddress(int id);
        Task<IList<AddressModel>> GetAddresses();
        Task<AddressModel> CreateAddress(AddressModel address);
    }
}