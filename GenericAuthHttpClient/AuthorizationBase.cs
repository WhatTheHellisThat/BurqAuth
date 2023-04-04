using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public abstract class AuthorizationBase
    {
        protected HttpClient client;
        protected HttpContent httpContent;
        protected HttpRequestMessage httpRequestMessage;

        public abstract Task<string> PostAsync();

        public abstract Task<string> GetAsync();

        protected abstract Task<string> RequestAsync(HttpRequestType httpRequestType);

        public string URL { get; set; }

        public AuthorizationBase(string URL)
        {
            this.URL = URL;
        }

        // Body

        public virtual void SetPlainBody(dynamic body)
        {
            if (body is null)
                throw new Exception("Body Can't be null");

            httpContent = new StringContent(body, null, "text/plain");
        }

        public virtual void SetJsonBody(dynamic body)
        {
            if (body is null)
                throw new Exception("Body Can't be null");

            httpContent = new StringContent(JsonConvert.SerializeObject(body), null, "application/json");
        }

        public virtual void SetXMLBody(dynamic body)
        {
            throw new NotImplementedException();
        }

        public virtual void SetURLEncoded(Dictionary<string, string> parameters)
        {
        }

        public virtual void SetHeader(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new Exception("Headers Can't be null");

            client.DefaultRequestHeaders.Add(key, value);
        }

        public virtual void SetHeaders(Dictionary<string, string> header)
        {
            if (header == null || header.Count < 1)
                throw new Exception("Headers Can't be null");

            foreach (var keyValuePair in header)
                SetHeader(keyValuePair.Key, keyValuePair.Value);
        }

        public virtual void ClearAndSetHeaders(Dictionary<string, string> header)
        {
            client.DefaultRequestHeaders.Clear();
            SetHeaders(header);
        }

        public virtual void SetParamters(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count < 1)
                throw new Exception("Headers Can't be null");

            List<KeyValuePair<string, string>> tempParamter = new List<KeyValuePair<string, string>>();

            foreach (var keyValuePair in parameters)
                tempParamter.Add(new KeyValuePair<string, string>(keyValuePair.Key, keyValuePair.Value));

            httpContent = new FormUrlEncodedContent(tempParamter.ToArray());
        }

        internal string ProcessResponse(HttpResponseMessage clientResponse)
        {
            clientResponse.EnsureSuccessStatusCode();
            return clientResponse.IsSuccessStatusCode ? clientResponse.Content.ReadAsStringAsync().Result : clientResponse.IsSuccessStatusCode.ToString();
        }
    }
}