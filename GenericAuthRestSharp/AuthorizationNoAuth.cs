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
        public virtual string URL { get; internal set; }

        public AuthorizationNoAuth(string url)
        {
            URL = url;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<AuthorizationNoAuth>().Build());
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            try
            {
                var client = new RestClient(URL);
                base.prepareRequest(restMethod);
                var restResponse = await client.ExecuteAsync(restRequest);
                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}