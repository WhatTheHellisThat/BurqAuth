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

        public string URL { get; set; }

        public AuthorizationBase(string URL)
        {
            this.URL = URL;
        }

        //// Body

        //public virtual void SetPlainBody(dynamic body)
        //{
        //    if (body is null)
        //        throw new Exception("Body Can't be null");

        //    httpContent = new StringContent(JsonConvert.SerializeObject(body), null, "text/plain");
        //}

        //public virtual void SetJsonBody(dynamic body)
        //{
        //    if (body is null)
        //        throw new Exception("Body Can't be null");

        //    httpContent = new StringContent(JsonConvert.SerializeObject(body), null, "application/json");
        //}

        //public virtual void SetXMLBody(dynamic body)
        //{
        //    if (body is null)
        //        throw new Exception("Body Can't be null");

        //    httpContent = new StringContent(JsonConvert.SerializeObject(body), null, "application/xml");
        //}

        //public virtual void SetURLEncoded(Dictionary<string, string> parameters)
        //{
        //}

        /// <summary>
        /// Set Single Header key Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public virtual void SetHeader(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new Exception("Headers Can't be null");

            restRequest.AddHeader(key, value);
        }

        public virtual void SetHeaders(Dictionary<string, string> header)
        {
            if (header == null || header.Count < 1)
                throw new Exception("Headers Can't be null");

            foreach (var keyValuePair in header)
                restRequest.AddHeaders(header);
        }

        public virtual void ClearAndSetHeaders(Dictionary<string, string> header)
        {
            restRequest.AddOrUpdateHeaders(header);
        }

        public virtual void SetParamter(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new Exception("Headers Can't be null");

            restRequest.AddOrUpdateParameter(key, value);
        }

        public virtual void SetParamters(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count < 1)
                throw new Exception("Headers Can't be null");

            restRequest.AddOrUpdateParameters((IEnumerable<Parameter>)parameters);
        }
    }
}