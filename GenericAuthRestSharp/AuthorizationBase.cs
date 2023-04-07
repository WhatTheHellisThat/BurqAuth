using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public abstract class AuthorizationBase
    {
        protected RestClient client;
        protected IRestResponse restResponse;
        protected RestRequest restRequest;
        public List<Header> headers { get; set; }

        public AuthorizationBase()
        {
            restRequest = new RestRequest();
        }

        public abstract Task<string> GetAsync();

        public abstract Task<string> PostAsync();

        protected abstract Task<string> RequestAsync(Method RestMethod);
    }
}