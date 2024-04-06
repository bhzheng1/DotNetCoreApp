using MediatR;
using ClassLibrary_MediatRDemo;

namespace ClassLibrary_MediatRDemo.Commands;
public record AddProductCommand(int Id, string Name) : IRequest<Product>;