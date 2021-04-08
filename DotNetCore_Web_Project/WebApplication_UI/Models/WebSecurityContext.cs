using System;
using Microsoft.AspNetCore.Http;

namespace WebApplication_UI.Models
{
    public class WebSecurityContext
    {
        private readonly IHeaderDictionary _header;

        public WebSecurityContext(IHttpContextAccessor contextAccessor)
        {
            _header = contextAccessor.HttpContext.Request.Headers;
        }

        public Guid UserId
        {
            get
            {
                if (!_header.ContainsKey("userid") || !Guid.TryParse(_header["userid"], out var userId))
                    return Guid.Empty;
                return userId;
            }
        }

        public Guid ClientId
        {
            get
            {
                if (!_header.ContainsKey("clientid") || !Guid.TryParse(_header["clientid"], out var clientId))
                    return Guid.Empty;
                return clientId;
            }
        }
    }
}
