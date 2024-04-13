using MediatR;
using MediatRDemo;

namespace MediatRDemo.Commands;
public record AddProductCommand(int Id, string Name) : IRequest<Product>;