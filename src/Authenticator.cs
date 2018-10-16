using System;
using System.Net.Http;
using System.Threading.Tasks;
using StuartDelivery.Models;
using StuartDelivery.Models.Token;

namespace StuartDelivery
{
    public class Authenticator
    {
        private readonly WebClient _webClient;
        private readonly Environment _environment;
        private OAuth2AccessToken oAuth2AccessToken;

        private string _clientId;
        private string _clientSecret;

        public Authenticator(Environment environment, string apiClientId, string apiClientSecret)
        {
            _webClient = new WebClient(environment);
            _environment = environment;
            _clientId = apiClientId;
            _clientSecret = apiClientSecret;
        }

        public async Task<string> GetAccessToken()
        {
            if (oAuth2AccessToken != null && !oAuth2AccessToken.IsExpired)
                return oAuth2AccessToken.AccessToken;
            else
            {
                oAuth2AccessToken = await GetNewAccessTokenAsync().ConfigureAwait(false);
                return oAuth2AccessToken.AccessToken;
            }
        }

        public async Task<OAuth2AccessToken> GetNewAccessTokenAsync()
        {
            var tokenRequest = new TokenRequest
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = "api",
                GrantType = "client_credentials"
            };

            var response = await _webClient.PostAsync("/oauth/token", tokenRequest).ConfigureAwait(false);

            TokenSuccessResponse tokenResponse;
            if (response.IsSuccessStatusCode)
                tokenResponse = await response.Content.ReadAsAsync<TokenSuccessResponse>().ConfigureAwait(false);
            else
                throw new HttpRequestException($"Access token request failed with message: {response.Content.ReadAsAsync<ErrorResponce>().Result.ErrorDescription}");

            return new OAuth2AccessToken()
            {
                AccessToken = tokenResponse.AccessToken,
                ExpireDate = DateTime.UtcNow.AddMinutes(tokenResponse.ExpiresIn),
                Scope = tokenResponse.Scope,
                TokenType = tokenResponse.TokenType
            };
        }
    }
}
