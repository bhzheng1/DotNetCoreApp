using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NServiceBus.MessageMutator;

namespace NSBus
{
    /// <summary>
    /// Injects security context into outgoing message headers
    /// If the message was send from handling an http request we extract from http headers
    /// If the message was send from nservicebus pipeline we extract from message headers
    /// </summary>
    internal class SecurityContextMutator : IMutateOutgoingTransportMessages
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly SecurityContextHeaderConfig _config;

        public SecurityContextMutator(IHttpContextAccessor accessor, SecurityContextHeaderConfig config)
        {
            _accessor = accessor;
            _config = config;
        }
        
        public Task MutateOutgoing(MutateOutgoingTransportMessageContext context)
        {
            IReadOnlyDictionary<string, string> sourceHeaders;
            
            if (_accessor.HttpContext == null)
                context.TryGetIncomingHeaders(out sourceHeaders);
            else
                sourceHeaders = _accessor.HttpContext.Request.Headers.ToDictionary(_ => _.Key, _ => _.Value.Single());
            
            context.OutgoingHeaders.Add(_config.UserIdHeaderName, sourceHeaders[_config.UserIdHeaderName]);
            context.OutgoingHeaders.Add(_config.ClientIdHeaderName, sourceHeaders[_config.ClientIdHeaderName]);

            return Task.CompletedTask;
        }
    }
}