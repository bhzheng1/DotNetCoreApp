using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace One.UI.HttpRequestHelper
{
    public class UserClientKeyProvider : IUserClientKeyProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public UserClientKeyProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid UserId()
        {
            //if (_accessor.HttpContext.Request.Headers["userid"].ToString() == string.Empty)
            //    return Guid.NewGuid();
            //else
            //    return new Guid(_accessor.HttpContext.Request.Headers["userid"]);
            //var claims = _accessor.HttpContext.User.Claims.ToList();
            //if (claims.Count > 0)
            //{
            //    string userId = claims[0].Value.ToString();
            //    return new Guid(userId);
            //}

            //var sessionProvider = CoreAuthMvcServiceFactory.SessionProviderMaker();
            //var currentUserId = sessionProvider.CurrentUserId(_accessor.HttpContext);

            //if(currentUserId.HasValue && Guid.TryParse(currentUserId.ValueOr(""), out var result))
            //    return result;

            //return Guid.NewGuid();
            var claims = _accessor.HttpContext.User.Claims.ToList();

            // Access claims
            foreach (var cItem in claims)
            {
                if (cItem.ValueType == "UserId")
                    return new Guid(cItem.Value.ToString());

            }
            return Guid.Empty;


        }
        public Guid ClientId()
        {
            //var claims = _accessor.HttpContext.User.Claims.ToList();
            //if (claims.Count > 2)
            //{
            //    string clientId = claims[1].Value.ToString();
            //    return new Guid(clientId);
            //}

            //return Guid.NewGuid();
            //return Guid.NewGuid();
            var claims = _accessor.HttpContext.User.Claims.ToList();

            // Access claims
            foreach (var cItem in claims)
            {
                if (cItem.ValueType == "ClientId")
                    return new Guid(cItem.Value.ToString());

            }
            return Guid.Empty;

        }
        public Guid SimulatedUserId()
        {
            //if (_accessor.HttpContext.Request.Headers["userid"].ToString() == string.Empty)
            //    return Guid.NewGuid();
            //else
            //    return new Guid(_accessor.HttpContext.Request.Headers["userid"]);
            var claims = _accessor.HttpContext.User.Claims.ToList();

            // Access claims
            foreach (var cItem in claims)
            {
                if (cItem.ValueType == "SimulatedUserId")
                    return new Guid(cItem.Value.ToString());
                
            }
            return Guid.Empty;

        }
       
    }


}
