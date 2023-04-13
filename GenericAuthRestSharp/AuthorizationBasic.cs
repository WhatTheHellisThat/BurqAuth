using Json.Schema;
using Json.Schema.Generation;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BurqAuthRestSharp
{
    public class AuthorizationBasic : AuthorizationBase
    {

        public  virtual string URL { get; internal set; }
        
        public virtual string UserName { get; internal set; }

        public virtual string Password { get; internal set; }

        public AuthorizationBasic(string url, string userName, string password)
        {
            URL = url;
            UserName = userName;
            Password = password;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<AuthorizationBasic>().Build());
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            try
            {
                client = new RestClient(URL);
                client.Authenticator = new HttpBasicAuthenticator(UserName, Password);
                base.prepareRequest(restMethod);
                restResponse = await client.ExecuteAsync(restRequest);
                return restResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}