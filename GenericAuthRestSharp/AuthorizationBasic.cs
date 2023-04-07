using Json.Schema;
using Json.Schema.Generation;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class AuthorizationBasic : AuthorizationBase
    {
        public new string URL { get; internal set; }
        public string UserName { get; internal set; }
        public string Password { get; internal set; }

        public AuthorizationBasic(string url, string userName, string password)
        {
            URL = url;
            UserName = userName;
            Password = password;
        }

        public override async Task<string> PostAsync()
        {
            return RequestAsync(Method.POST).Result;
        }

        public override async Task<string> GetAsync()
        {
            return RequestAsync(Method.GET).Result;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<AuthorizationBasic>().Build());
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            string response = string.Empty;
            try
            {
                client = new RestClient(URL);
                client.Authenticator = new HttpBasicAuthenticator(UserName, Password);

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