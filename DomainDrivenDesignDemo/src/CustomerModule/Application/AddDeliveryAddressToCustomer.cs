using MediatR;

namespace Application;

public class AddDeliveryAddressToCustomer : IRequest
{
    public int CustomerId { get; set; }
    public string StreetName { get; set; }
    public int StreetNumber { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}