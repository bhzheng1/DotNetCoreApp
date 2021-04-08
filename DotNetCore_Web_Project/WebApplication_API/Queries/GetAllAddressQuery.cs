using MediatR;
using ModelClassLibrary;
using System.Collections.Generic;

namespace WebApplication_API.Queries
{
    public class GetAllAddressQuery:IRequest<List<AddressModel>>
    {
    }
}
