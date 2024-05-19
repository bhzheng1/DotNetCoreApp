using Domain.Aggregates;
using Infrastructure;
using MediatR;

namespace Application;

public class CreateCustomerHandler(AppDbContext ctx)
    : IRequestHandler<CreateCustomer, Customer>
{
    private readonly AppDbContext _ctx = ctx;

    public async Task<Customer> Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        var name = new Name(request.FirstName, request.LastName);
        var customer = new Customer(0, name);
        _ctx.Customers.Add(customer);
        await _ctx.SaveChangesAsync(cancellationToken);
        return customer;
    }
}