using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace Market.xUnit.Integration.Configurations
{
    public abstract class BaseTestEntity
    {
        protected TestServer Server = new ServerApiFactory().Server;

        public HttpClient CreateHttpClient()
        {
            var client = Server.CreateClient();
            return client;
        }

        public HttpClient CreateHttpClientWithToken()
        {
            var client = Server.CreateClient();
            var token = GetUserToken(client).GetAwaiter().GetResult();
            client.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.Authorization),
                $"{JwtBearerDefaults.AuthenticationScheme} {token.AccessToken}");
            //client.DefaultRequestHeaders.Add(HttpRequestHeaderNames.ApiKey, ApiKeyAuthenticationHandler.DefaultApiKeyValue);

            return client;
        }

        private async Task<TokenInfo> GetUserToken(HttpClient client)
        {
            var login = new LoginInfo
            {
                Username = "testuser",
                Password = "testpassword"
            };
            var response = await client.PostAsJsonAsync("api/auth/login", login);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadFromJsonAsync<TokenInfo>();

            return token;
        }
    }
}
