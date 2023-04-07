namespace BurqAuthRestSharp
{
    public class ShopifyBasicAuth : AuthorizationBasic
    {
        public ShopifyBasicAuth(string url, string userName, string password) : base(url, userName, password)
        {
        }
    }
}