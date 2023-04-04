using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class AuthorizationBasic : AuthorizationBase
    {
        public readonly string UserName;
        public readonly string Password;

        public AuthorizationBasic(string URL, string userName, string password) : base(URL)
        {
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