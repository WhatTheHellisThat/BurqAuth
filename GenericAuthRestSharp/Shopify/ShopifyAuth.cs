using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.Shopify
{
    public class ShopifyAuth : AuthorizationBase
    {
        private AuthorizationBasic AuthorizationBasic;
        private AuthorizationToken AuthorizationToken;
        private AuthorizationType authorizationType;
        public readonly string Token;
        public readonly string Version;
        public readonly string StoreName;

        private static string URLFormatOld = "https://{0}:{1}@{2}.myshopify.com/admin/api/{4}/orders.json";
        private string PrepareURLOld()
        {
            return string.Format(URLFormatOld, AuthorizationBasic.UserName, AuthorizationBasic.Password, StoreName, Version);
        }
        private static string URLFormatNew = "https://{0}.myshopify.com/admin/orders.json";
        private string PrepareURLNew()
        {
            return string.Format(URLFormatNew, StoreName);
        }

        //public ShopifyAuth(string username, string password, string storeName, string version) : base(storeName)
        //{
        //    Version = version;
        //    authorizationType = AuthorizationType.Basic;
        //    AuthorizationBasic = new AuthorizationBasic(storeName, username, password);
        //    AuthorizationBasic.URL = PrepareURL();
        //}
        //
        //public ShopifyAuth(AuthorizationBasic authorizationBasic, string storeName, string version) : base(authorizationBasic.URL)
        //{
        //    Version = version;
        //    authorizationType = AuthorizationType.Basic;
        //    AuthorizationBasic = authorizationBasic;
        //    AuthorizationBasic.URL = PrepareURL();
        //}

        public ShopifyAuth(string storeName, string token) : base(storeName)
        {
            StoreName = storeName;
            Token = token;
            AuthorizationToken = new AuthorizationToken(storeName, Token);
            AuthorizationToken.URL = PrepareURLNew();
        }

        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(ShopifyAuth)).ToString();
        }

        public override async Task<string> GetAsync()
        {
            Task<string> response = null;
            AuthorizationToken.SetHeader("X-Shopify-Access-Token", Token);
            await (response = AuthorizationToken.GetAsync());
            return response.Result;
        }

        public override async Task<string> PostAsync()
        {
            Task<string> response = null;
            AuthorizationToken.SetHeader("X-Shopify-Access-Token", Token);
            await (response = AuthorizationToken.PostAsync());
            return response.Result;
        }

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new NotImplementedException();
        }

    }
}