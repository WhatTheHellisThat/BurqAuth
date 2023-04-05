using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.Magento
{
    public class MagentoAuth : AuthorizationBase
    {
        public string UserName { get; internal set; }
        public string Password { get; internal set; }
        public new string URL { get; internal set; }

        AuthorizationNoAuth _authorizationNoAuth;

        public MagentoAuth(string url, string userName, string password) : base(url)
        {
            UserName = userName;
            Password = password;
            URL = url;
            _authorizationNoAuth = new AuthorizationNoAuth(url);
        }

        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(MagentoAuth)).ToString();
        }

        public override async Task<string> GetAsync()
        {
            _authorizationNoAuth.SetJsonBody(new { username = UserName, password = Password });
            return await _authorizationNoAuth.GetAsync();
        }

        public override async Task<string> PostAsync()
        {
            _authorizationNoAuth.SetJsonBody(new { username = UserName, password = Password });
            return await _authorizationNoAuth.PostAsync();
        }

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}
