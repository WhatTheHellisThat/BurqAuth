using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public class AuthorizationToken : AuthorizationBase
    {
        private string Token { get; set; }

        public AuthorizationToken(string URL, string token) : base(URL)
        {
            Token = token.Replace('"', ' ').Trim();
        }

        public override async Task<string> GetAsync()
        {
            return RequestAsync(HttpRequestType.GET).Result;
        }

        public override async Task<string> PostAsync()
        {
            return RequestAsync(HttpRequestType.POST).Result;
        }

        protected override async Task<string> RequestAsync(HttpRequestType httpRequestType)
        {
            string response = string.Empty;
            try
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                using (client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    Task<HttpResponseMessage> clientInfo = null;
                    switch (httpRequestType)
                    {
                        case HttpRequestType.GET:
                            await (clientInfo = client.GetAsync(client.BaseAddress));
                            break;

                        case HttpRequestType.POST:
                            await (clientInfo = client.PostAsync(client.BaseAddress, httpContent));
                            break;

                        default:
                            break;
                    }
                    response = base.ProcessResponse(clientInfo.Result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return response;
        }
    }
}