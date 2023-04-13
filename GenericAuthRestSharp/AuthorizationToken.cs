using Json.Schema;
using Json.Schema.Generation;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class AuthorizationToken : AuthorizationBase
    {
        public virtual string URL { get; internal set; }

        public virtual string Token { get; internal set; }

        public AuthorizationToken(string url, string token)
        {
            URL = url;
            Token = token.Replace('"', ' ').Trim();
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
                base.prepareRequest(restMethod);
                var restResponse = await client.ExecuteAsync(restRequest);

                if (!restResponse.IsSuccessful)
                    throw new Exception($"Request failed with status code {restResponse.StatusCode}: {restResponse.ErrorMessage}");

                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Request failed: {ex.Message}", ex);
            }
        }
    }
}