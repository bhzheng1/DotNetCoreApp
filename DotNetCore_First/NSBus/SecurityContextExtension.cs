using NServiceBus;
using NServiceBus.Pipeline;

namespace NSBus
{
    public static class SecurityContextExtension
    {
        public static SecurityContext GetSecurityContext(this IMessageHandlerContext context)
        {
            return context.Extensions.Get<SecurityContext>();
        }

        public static SecurityContext GetSecurityContext(this IIncomingLogicalMessageContext context)
        {
            return context.Extensions.Get<SecurityContext>();
        }
    }
}