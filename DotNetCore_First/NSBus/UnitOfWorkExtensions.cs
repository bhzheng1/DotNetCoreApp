using Microsoft.EntityFrameworkCore;
using NServiceBus;

namespace NSBus
{
    public static class UnitOfWorkExtensions
    {
        public static T GetDbContext<T>(this IMessageHandlerContext context) where T : DbContext
        {
            return context.Extensions.Get<UnitOfWorkProvider<T>>().GetDataContext(context.SynchronizedStorageSession);
        }
    }
}