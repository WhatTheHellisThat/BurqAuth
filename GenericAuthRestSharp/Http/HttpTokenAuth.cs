namespace BurqAuthRestSharp
{
    public class HttpTokenAuth : AuthorizationToken
    {
        public HttpTokenAuth(string url, string token) : base(url, token)
        {
        }
    }
}