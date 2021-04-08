using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

namespace NSBus
{
    public static class RequestLocalExtension
    {
        public static Task<TResponse> RequestLocal<TResponse>(this IMessageSession session, object requestMessage)
        {
            var options = new SendOptions();
            options.RouteToThisEndpoint();
            return session.Request<TResponse>(requestMessage, options, CancellationToken.None);
        }
    }
}
