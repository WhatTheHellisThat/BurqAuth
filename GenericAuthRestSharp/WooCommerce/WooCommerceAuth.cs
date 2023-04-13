using Json.Schema;
using Json.Schema.Generation;
using System.Text.Json;

namespace BurqAuthRestSharp
{
    public class WooCommerceAuth : AuthorizationBasic
    {
        public WooCommerceAuth(string url, string username, string password) : base(url, username, password)
        {
            UserName = username;
            Password = password;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<WooCommerceAuth>().Build());
        }
    }
}