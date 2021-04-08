using System;
using Microsoft.AspNetCore.Http;

namespace NSBus
{
    public class SecurityContextProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public SecurityContextProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public SecurityContext GetSecurityContext()
        {
            var headers = _accessor.HttpContext.Request.Headers;
            if(!headers.ContainsKey("userId")||!Guid.TryParse(headers["userId"],out var userId))
                throw new Exception("missing valid userId header");
            if (!headers.ContainsKey("clientId") || !Guid.TryParse(headers["clientId"], out var clientId))
                throw new Exception("missing valid clientId header");
            return new SecurityContext(userId,clientId);
        }
    }
}
