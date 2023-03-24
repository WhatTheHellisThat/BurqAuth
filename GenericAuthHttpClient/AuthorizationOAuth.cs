using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public class AuthorizationOAuth : AuthorizationBase
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string code;
        private readonly string redirectUri;
        private readonly string accessTokenUrl;
        private readonly string refereshTokenUrl;
        public AuthorizationResponse authorizationResponse;

        public AuthorizationOAuth(string clientId, string clientSecret, string code, string accessTokenUrl, string refereshTokenUrl) : base(accessTokenUrl)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.code = code;
            this.redirectUri = redirectUri;
            this.accessTokenUrl = accessTokenUrl;
            this.refereshTokenUrl = refereshTokenUrl;
        }

        public override async Task<string> GetAsync()
        {
            return await RequestAsync(HttpRequestType.GET);
        }

        public override async Task<string> PostAsync()
        {
            return await RequestAsync(HttpRequestType.POST);
        }

        private async Task<string> GenerateAccessToken()
        {
            string response = string.Empty;
            try
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(accessTokenUrl);
                var parameters = new Dictionary<string, string>
                {
                    { "client_id", this.clientId },
                    { "client_secret", this.clientSecret },
                    { "code", this.code }
                };
                this.SetParamters(parameters);
                this.SetHeader("Accept", "application/json");
                using (client)
                {
                    Task<HttpResponseMessage> clientInfo = null;
                    clientInfo = client.PostAsync(client.BaseAddress, httpContent);
                    response = base.ProcessResponse(clientInfo.Result);
                    authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(response);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return response;
        }

        private async Task<string> GenerateRefershToken()
        {
            string response = string.Empty;
            try
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(refereshTokenUrl);
                var parameters = new Dictionary<string, string>
                {
                    { "client_id", this.clientId },
                    { "client_secret", this.clientSecret },
                    { "refresh_token", this.authorizationResponse.refresh_token },
                    { "grant_type", "refresh_token" }
                };
                this.SetParamters(parameters);
                using (client)
                {
                    Task<HttpResponseMessage> clientInfo = null;
                    await (clientInfo = client.PostAsync(client.BaseAddress, httpContent));
                    response = base.ProcessResponse(clientInfo.Result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return response;
        }

        protected override Task<string> RequestAsync(HttpRequestType httpRequestType)
        {
            return GenerateAccessToken();
        }
    }
}