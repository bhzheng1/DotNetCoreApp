using MediatR;
namespace MediatRDemo.Queries;
public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
public record GetProductById(int Id) : IRequest<Product>;