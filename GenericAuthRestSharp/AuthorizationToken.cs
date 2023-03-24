using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
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
            return RequestAsync(Method.GET).Result;
        }

        public override async Task<string> PostAsync()
        {
            return RequestAsync(Method.POST).Result;
        }

        protected override async Task<string> RequestAsync(Method restMethod)
        {
            string response = string.Empty;
            try
            {
                client = new RestClient(URL)
                {
                    Authenticator = new JwtAuthenticator(Token)
                };
                restRequest = new RestRequest(restMethod);
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