using System;

namespace NSBus
{
    public class SecurityContext
    {
        public SecurityContext(Guid userId, Guid clientId)
        {
            UserId = userId;
            ClientId = clientId;
        }
        
        public Guid UserId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}