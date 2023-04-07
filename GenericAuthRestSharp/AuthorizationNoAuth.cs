using Json.Schema;
using Json.Schema.Generation;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class AuthorizationNoAuth : AuthorizationBase
    {
        public string URL { get; internal set; }

        public AuthorizationNoAuth(string url)
        {
            URL = url;
        }

        public override async Task<string> GetAsync()
        {
            return RequestAsync(Method.GET).Result;
        }

        public override async Task<string> PostAsync()
        {
            return RequestAsync(Method.POST).Result;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<AuthorizationNoAuth>().Build());
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            string response = string.Empty;
            try
            {
                client = new RestClient(URL);
                if (restRequest is null)
                    restRequest = new RestRequest(restMethod);
                else
                    restRequest.Method = restMethod;

                restResponse = await client.ExecuteAsync(restRequest);
                response = restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return response;
        }
    }
}