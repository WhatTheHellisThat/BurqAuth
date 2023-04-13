using Json.Schema.Generation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public abstract class AuthorizationBase
    {
        protected RestClient client;
        protected IRestResponse restResponse;
        protected RestRequest restRequest;

        [JsonExclude]
        public List<KeyValuePair<string, string>> QueryParameters { get; set; }
        [JsonExclude]
        public List<KeyValuePair<string, string>> Headers { get; set; }

        [JsonExclude]
        public string ContentType { get; set; }
        [JsonExclude]
        public string Body { get; set; }

        public AuthorizationBase()
        {
            restRequest = new RestRequest();
        }

        public virtual string Get()
        { return RequestAsync(Method.GET).Result; }

        public virtual string Post()
        {
            return RequestAsync(Method.POST).Result;
        }

        protected virtual Task<string> RequestAsync(Method RestMethod)
        { throw new NotImplementedException(); }

        internal void prepareRequest(Method restMethod)
        {
            if (restRequest == null)
                restRequest = new RestRequest(restMethod);
            else
                restRequest.Method = restMethod;

            if (!string.IsNullOrEmpty(Body))
                if (ContentType.ToLower().Contains("json"))
                    restRequest.AddJsonBody(Body);
                else restRequest.AddXmlBody(Body);

            if (Headers != null && Headers.Any())
                Headers.ForEach(kvp => restRequest.AddHeader(kvp.Key, kvp.Value));

            if (QueryParameters != null && QueryParameters.Any())
                QueryParameters.ForEach(q => restRequest.AddQueryParameter(q.Key, q.Value));
        }
    }
}