using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.WooCommerce
{
    public class WooCommerceAuth : AuthorizationBase
    {
        public AuthorizationBasic AuthorizationBasic;

        public WooCommerceAuth(string URL, string consumerkey, string consumerSecret) : base(URL)
        {
            AuthorizationBasic = new AuthorizationBasic(URL, consumerkey, consumerSecret);
        }

        public WooCommerceAuth(AuthorizationBasic authorizationBasic) : base(authorizationBasic.URL)
        {
            AuthorizationBasic = authorizationBasic;
        }

        public override async Task<string> GetAsync()
        {
            Task<string> response = null;
            await (response = AuthorizationBasic.GetAsync());
            return response.Result;
        }


        public override async Task<string> PostAsync()
        {
            Task<string> response = null;
            await (response = AuthorizationBasic.PostAsync());
            return response.Result; ;
        }

        protected override Task<string> RequestAsync(Method RestMethod)
        {
            throw new NotImplementedException();
        }
        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(WooCommerceAuth)).ToString();
        }

    }
}
