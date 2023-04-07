namespace BurqAuthRestSharp
{
    public class HttpBasicAuth : AuthorizationBasic
    {
        public HttpBasicAuth(string url, string userName, string password) : base(url, userName, password)
        {
        }
    }
}