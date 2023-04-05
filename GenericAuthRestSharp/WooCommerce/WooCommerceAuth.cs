using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.WooCommerce
{
    public class WooCommerceAuth : AuthorizationBase
    {
        public AuthorizationBasic AuthorizationBasic;

        public WooCommerceAuth(string url, string consumerKey, string consumerSecret) : base(url)
        {
            AuthorizationBasic = new AuthorizationBasic(url, consumerKey, consumerSecret);
        }

        public WooCommerceAuth(AuthorizationBasic authorizationBasic) : base(authorizationBasic.URL)
        {
            AuthorizationBasic = authorizationBasic;
        }

        public override async Task<string> GetAsync()
        {
            return await AuthorizationBasic.GetAsync();
        }

        public override async Task<string> PostAsync()
        {
            return await AuthorizationBasic.PostAsync();
        }

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new NotImplementedException();
        }

        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(WooCommerceAuth)).ToString();
        }
    }
}