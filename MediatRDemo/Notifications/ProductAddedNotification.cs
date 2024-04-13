using MediatR;

namespace MediatRDemo.Notifications;
public record ProductAddedNotification(Product Product) : INotification;