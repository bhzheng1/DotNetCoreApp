using System;
using System.Threading.Tasks;
using NSBus;
using NServiceBus.Pipeline;

namespace One.API.dbo.Permission
{
    public class RolePermissionsSetup : Behavior<IIncomingLogicalMessageContext>
    {
        public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
        {
            var serviceContext = context.GetSecurityContext();
            var roleContext = new SecurityContext(serviceContext.UserId, serviceContext.ClientId);
            IRolePermissions rolePermissions = new RolePermissions(roleContext);
            context.Extensions.Set(rolePermissions);
            await next();
            context.Extensions.Remove<IRolePermissions>();
        }
    }
}