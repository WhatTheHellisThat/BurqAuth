using Json.Schema;
using Json.Schema.Generation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BurqAuthRestSharp
{
    public class ShopifyTokenAuth : AuthorizationToken
    {
        [JsonExclude]
        public override string URL { get; internal set; }

        public string StoreName { get; internal set; }

        private static readonly string urlFormat = "https://{0}.myshopify.com/admin/orders.json";

        public ShopifyTokenAuth(string storeName, string token) : base(storeName, token)
        {
            StoreName = storeName;
            Token = token;
            base.URL = string.Format(urlFormat, StoreName);
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<ShopifyTokenAuth>().Build());
        }

        public override string Get()
        {
            restRequest.AddHeader("X-Shopify-Access-Token", Token);
            return base.Get();
        }
    }
}