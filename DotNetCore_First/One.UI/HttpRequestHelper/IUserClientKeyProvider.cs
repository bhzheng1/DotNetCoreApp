using System;

namespace One.UI.HttpRequestHelper
{
    public interface IUserClientKeyProvider
    {
        Guid UserId();
        Guid ClientId();
        Guid SimulatedUserId();
    }
}
