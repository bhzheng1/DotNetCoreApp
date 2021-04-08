using MediatR;
using ModelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_API.Queries;
using WebApplication_API.Repositories;

namespace WebApplication_API.Handlers
{
    public class GetAllAddressHandler : IRequestHandler<GetAllAddressQuery, List<AddressModel>>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAllAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<List<AddressModel>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            var result = await _addressRepository.GetAddresses();
            return result.ToList();
        }
    }
}
