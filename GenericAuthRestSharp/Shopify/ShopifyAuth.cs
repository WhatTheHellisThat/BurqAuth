using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using NJsonSchema;
using RestSharp;
using System;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.Shopify
{
    public class ShopifyAuth : AuthorizationBase
    {
        private readonly AuthorizationToken _authorizationToken;
        public string StoreName { get; internal set; }
        public string Token { get; internal set; }

        private static readonly string urlFormat = "https://{0}.myshopify.com/admin/orders.json";

        public ShopifyAuth(string storeName, string token) : base(storeName)
        {
            StoreName = storeName;
            Token = token;
            _authorizationToken = new AuthorizationToken(storeName, token);
            _authorizationToken.URL = string.Format(urlFormat, StoreName);
        }

        public static string GetMetaData()
        {
            //return JsonSchema.FromType<ShopifyAuth>().ToJson();
            return new JSchemaGenerator().Generate(typeof(ShopifyAuth)).ToString();
        }

        public override async Task<string> GetAsync()
        {
            _authorizationToken.SetHeader("X-Shopify-Access-Token", Token);
            return await _authorizationToken.GetAsync();
        }

        public override async Task<string> PostAsync()
        {
            _authorizationToken.SetHeader("X-Shopify-Access-Token", Token);
            return await _authorizationToken.PostAsync();
        }

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new NotImplementedException();
        }
    }
}