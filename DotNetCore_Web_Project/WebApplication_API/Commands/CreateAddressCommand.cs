using MediatR;
using ModelClassLibrary;

namespace WebApplication_API.Commands
{
    public class CreateAddressCommand:IRequest<AddressModel>
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
    }
}
