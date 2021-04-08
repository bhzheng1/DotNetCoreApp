using MediatR;
using ModelClassLibrary;

namespace WebApplication_API.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressModel>
    {
        public int Id { get; set; }
        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
