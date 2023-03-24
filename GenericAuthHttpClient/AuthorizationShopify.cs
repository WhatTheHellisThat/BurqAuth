using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public class AuthorizationShopify : AuthorizationBase
    {
        public AuthorizationShopify(string storeUrl) : base(storeUrl)
        {
        }

        public virtual List<AuthorizationType> FetchSupportedAuth()
        {
            return new List<AuthorizationType>() { AuthorizationType.Basic, AuthorizationType.Token };
        }

        public override Task<string> GetAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<string> PostAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<string> RequestAsync(HttpRequestType httpRequestType)
        {
            throw new NotImplementedException();
        }
    }
}