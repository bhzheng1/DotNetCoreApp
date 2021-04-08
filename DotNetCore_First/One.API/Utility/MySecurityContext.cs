using System;
using Microsoft.AspNetCore.Http;

namespace One.API.Utility
{
    public class MySecurityContext
    {
        private readonly IHeaderDictionary _header;

        public MySecurityContext(IHttpContextAccessor contextAccessor)
        {
            _header = contextAccessor.HttpContext.Request.Headers;
        }

        public Guid UserId
        {
            get
            {
                if(!_header.ContainsKey("userid")||!Guid.TryParse(_header["userid"],out var userId))
                    throw new Exception("missing valid userid header");
                return userId;
            }
        }

        public Guid ClientId
        {
            get
            {
                if (!_header.ContainsKey("clientid") || !Guid.TryParse(_header["clientid"], out var clientId))
                    throw new Exception("missing valid clientid header");
                return clientId;
            }
        }
    }
}
