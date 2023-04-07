using Json.Schema;
using Json.Schema.Generation;

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
            return new JsonSchemaBuilder().FromType<WooCommerceAuth>().Build().ToString();
        }
    }
}