using MediatR;
using MediatRDemo.Queries;

namespace MediatRDemo.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
{
    private readonly FakeDataStore _fakeDataStore;
    public GetProductByIdHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }

    public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        return await _fakeDataStore.GetProductById(request.Id);
    }
}
