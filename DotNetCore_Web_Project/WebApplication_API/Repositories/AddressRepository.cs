using AutoMapper;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;
using WebApplication_API.SakilaModels;

//本例演示Task, Automapper
namespace WebApplication_API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SakilaContextMSSQL _dbContext;
        private readonly IMapper _mapper;
        public AddressRepository(SakilaContextMSSQL sakila, IMapper mapper)
        {
            _dbContext = sakila;
            _mapper = mapper;
        }
        public Task<AddressModel> GetAddress(int id)
        {
            var address = _dbContext.Address.Where(_ => _.AddressId == id).SingleOrDefault();
            return Task.FromResult(_mapper.Map<AddressModel>(address));
        }

        public Task<IList<AddressModel>> GetAddresses()
        {
            var addresses = _dbContext.Address.ToList();
            return Task.FromResult(_mapper.Map<IList<AddressModel>>(addresses));
        }

        public Task<AddressModel> CreateAddress(AddressModel address)
        {
            var _address = new Address
            {
                Address1 = address.Address1,
                Address2 = address.Address2
            };
            try
            {
                _dbContext.Address.Add(_address);
                _dbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }

            return Task.FromResult(address);
        }
    }
}
