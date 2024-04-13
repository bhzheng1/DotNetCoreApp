using MediatR;
using MediatRDemo.Notifications;

namespace MediatRDemo.Handlers;

public class CacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
{
    private readonly FakeDataStore _fakeDataStore;
    public CacheInvalidationHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }
    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _fakeDataStore.EventOccurred(notification.Product, "Cache invalidated");
        await Task.CompletedTask;
    }
}