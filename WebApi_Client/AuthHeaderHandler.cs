using System.Net.Http.Headers;
namespace WebApi_Client
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ITokenApiClient _tokenApiClient;
        public AuthHeaderHandler(ITokenApiClient client)
        {
            _tokenApiClient = client;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            try
            {
                var token = await _tokenApiClient.FetchTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

