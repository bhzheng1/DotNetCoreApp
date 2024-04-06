using MediatR;
namespace ClassLibrary_DataAccess.Queries;

public record GetProductListQuery() : IRequest<GetProductListQueryResponse>;

public record GetProductListQueryResponse(IEnumerable<ProductModel> Products);

public record ProductModel(int Id, string Name, decimal Price);
