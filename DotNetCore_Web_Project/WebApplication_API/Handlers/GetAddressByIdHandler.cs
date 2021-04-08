using MediatR;
using ModelClassLibrary;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_API.Queries;
using WebApplication_API.Repositories;

namespace WebApplication_API.Handlers
{
    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, AddressModel>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressByIdHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<AddressModel> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _addressRepository.GetAddress(request.Id);
            return result;
        }
    }
}
