using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurqAuthRestSharp
{
    public abstract class AuthorizationBase
    {
        protected RestClient client;
        protected IRestResponse restResponse;
        protected RestRequest restRequest;

        public abstract Task<string> PostAsync();

        public abstract Task<string> GetAsync();

        protected abstract Task<string> RequestAsync(Method RestMethod);

        [JsonIgnore]
        public string URL { get; set; }

        public AuthorizationBase(string URL)
        {
            this.URL = URL;
            restRequest = new RestRequest();
        }

        #region Body

        public virtual void SetPlainBody(string body)
        {
            if (body is null)
                throw new Exception("Body Can't be null");

            restRequest.AddBody(body, "text/plain");
        }

        public virtual void SetJsonBody(dynamic body)
        {
            if (body is null)
                throw new Exception("Body Can't be null");

            string json = JsonConvert.SerializeObject(body);
            restRequest.AddJsonBody(json, "application/json");
        }

        public virtual void SetXMLBody(dynamic body)
        {
            throw new NotImplementedException();
        }

        public virtual void SetURLEncoded(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion Body

        #region Header

        public virtual void SetHeader(string key, string value)
        {
            restRequest.AddOrUpdateHeader(key, value);
        }

        public virtual void SetHeaders(Dictionary<string, string> header)
        {
            restRequest.AddOrUpdateHeaders(header);
        }

        #endregion Header

        #region Parameters

        public virtual void SetParamter(string key, string value)
        {
            restRequest.AddOrUpdateParameter(key, value);
        }

        public virtual void SetParamters(Dictionary<string, string> parameters)
        {
            restRequest.AddOrUpdateParameters((IEnumerable<Parameter>)parameters);
        }

        #endregion Parameters
    }
}