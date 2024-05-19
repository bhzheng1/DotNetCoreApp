using Domain.Aggregates;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class AddDeliveryAddressToCustomerHandler(AppDbContext ctx) : IRequestHandler<AddDeliveryAddressToCustomer>
{
    private readonly AppDbContext _ctx = ctx;
    public async Task Handle(AddDeliveryAddressToCustomer request, CancellationToken cancellationToken)
    {
        var address = new Address(request.StreetName, request.StreetNumber, request.City, request.Country);
        var customer = await _ctx.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);
        if (customer is null) return;
        
        customer.AddDeliveryAddress(address);
        _ctx.Customers.Update(customer);
        await _ctx.SaveChangesAsync(cancellationToken);
    }
}