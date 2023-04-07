using Json.Schema;
using Json.Schema.Generation;
using System.Text.Json;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public class MagentoAuth : AuthorizationNoAuth
    {
        public new string URL { get; internal set; }
        public string UserName { get; internal set; }
        public string Password { get; internal set; }

        public MagentoAuth(string url, string userName, string password) : base(url)
        {
            UserName = userName;
            Password = password;
            URL = url;
        }

        public static string GetMetaData()
        {
            return JsonSerializer.Serialize(new JsonSchemaBuilder().FromType<MagentoAuth>().Build());
        }

        public override async Task<string> PostAsync()
        {
            restRequest.AddJsonBody(new { username = UserName, password = Password });
            return await base.PostAsync();
        }
    }
}