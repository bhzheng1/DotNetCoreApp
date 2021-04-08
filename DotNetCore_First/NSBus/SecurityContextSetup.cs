using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;

namespace NSBus
{
    internal class SecurityContextSetup : Behavior<IIncomingLogicalMessageContext>
    {
        private readonly SecurityContextHeaderConfig _config;

        public SecurityContextSetup(SecurityContextHeaderConfig config)
        {
            _config = config;
        }

        public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
        {
            var userId = Guid.Parse(context.Headers[_config.UserIdHeaderName]);
            var clientId = Guid.Parse(context.Headers[_config.ClientIdHeaderName]);
            var securityContext = new SecurityContext(userId, clientId);
            context.Extensions.Set(securityContext);
            await next();
            context.Extensions.Remove<SecurityContext>();
        }
    }
}