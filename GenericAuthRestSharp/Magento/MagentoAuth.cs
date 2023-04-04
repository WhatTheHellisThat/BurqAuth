using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.Magento
{
    public class MagentoAuth : AuthorizationBase
    {
        public readonly string UserName;
        public readonly string Password;
        private AuthorizationNoAuth AuthorizationNoAuth;



        public MagentoAuth(string URL, string userName, string password) : base(URL)
        {
            UserName = userName;
            Password = password;
            AuthorizationNoAuth = new AuthorizationNoAuth(URL);
        }

        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(MagentoAuth)).ToString();
        }

        public override async Task<string> GetAsync()
        {
            Task<string> response = null;
            await (response = AuthorizationNoAuth.GetAsync());
            return response.Result;
        }

        public override async Task<string> PostAsync()
        {
            Task<string> response = null;
            AuthorizationNoAuth.SetJsonBody(new { username = UserName, password = Password });
            await (response = AuthorizationNoAuth.PostAsync());
            return response.Result;

        }

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new NotImplementedException();
        }

    }
}