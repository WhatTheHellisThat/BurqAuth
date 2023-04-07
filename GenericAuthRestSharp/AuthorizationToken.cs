using Json.Schema;
using Json.Schema.Generation;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class AuthorizationToken : AuthorizationBase
    {
        [JsonIgnore]
        public string URL { get; internal set; }

        public string Token { get; internal set; }

        public AuthorizationToken(string url, string token)
        {
            URL = url;
            Token = token.Replace('"', ' ').Trim();
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
            return System.Text.Json.JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<AuthorizationToken>().Build());
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            try
            {
                var client = new RestClient(URL);
                client.Authenticator = new JwtAuthenticator(Token);

                if (headers?.Any() == true)
                {
                    foreach (var header in headers)
                    {
                        restRequest.AddHeader(header.key, header.value);
                    }
                }

                if (restRequest == null)
                    restRequest = new RestRequest(restMethod);
                else
                    restRequest.Method = restMethod;

                var restResponse = await client.ExecuteAsync(restRequest);

                if (!restResponse.IsSuccessful)
                {
                    throw new Exception($"Request failed with status code {restResponse.StatusCode}: {restResponse.ErrorMessage}");
                }

                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Request failed: {ex.Message}", ex);
            }
        }
    }
}