using ClassLibrary_DataAccess.Queries;
using MediatR;
using Module_WorldDemo.DataAccess;

namespace ClassLibrary_DataAccess.Handlers;
public class GetProductListHandler : IRequestHandler<GetProductListQuery, GetProductListQueryResponse>
{
    private readonly IProductRepository _productRepository;
    public GetProductListHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<GetProductListQueryResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts();
        return new GetProductListQueryResponse(products.Select(p => new ProductModel(p.Id, p.Name, p.Price)));
    }
}