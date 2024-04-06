using MediatR;

namespace ClassLibrary_MediatRDemo.Notifications;
public record ProductAddedNotification(Product Product) : INotification;