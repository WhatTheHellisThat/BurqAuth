using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public class AuthorizationNoAuth : AuthorizationBase
    {
        public AuthorizationNoAuth(string URL) : base(URL)
        {
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
                    Task<HttpResponseMessage> clientInfo = null;
                    switch (httpRequestType)
                    {
                        case HttpRequestType.GET:
                            await (clientInfo = client.GetAsync(client.BaseAddress));
                            break;

                        case HttpRequestType.POST:
                            await (clientInfo = client.PostAsync(client.BaseAddress, httpContent));
                            break;

                        case HttpRequestType.PUT:
                            await (clientInfo = client.PutAsync(client.BaseAddress, httpContent));
                            break;

                        case HttpRequestType.DELETE:
                            await (clientInfo = client.DeleteAsync(client.BaseAddress));
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