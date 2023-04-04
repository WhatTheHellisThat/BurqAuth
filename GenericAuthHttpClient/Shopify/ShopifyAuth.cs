using System;
using System.Threading.Tasks;

namespace BurqAuthHttpClient.Shopify
{
    public class ShopifyAuth : AuthorizationBase
    {
        private AuthorizationBasic AuthorizationBasic;
        private AuthorizationToken AuthorizationToken;
        private AuthorizationType authorizationType;

        public ShopifyAuth(AuthorizationBasic authorizationBasic) : base(authorizationBasic.URL)
        {
            authorizationType = AuthorizationType.Basic;
            AuthorizationBasic = authorizationBasic;
        }

        public ShopifyAuth(AuthorizationToken authorizationToken) : base(authorizationToken.URL)
        {
            authorizationType = AuthorizationType.Basic;
            AuthorizationToken = authorizationToken;
        }


        public override async Task<string> GetAsync()
        {
            Task<string> response = null;
            switch (authorizationType)
            {
                case AuthorizationType.Basic:
                    await (response = AuthorizationBasic.GetAsync());
                    break;

                case AuthorizationType.Token:
                    await (response = AuthorizationToken.GetAsync());
                    break;

                default:
                    break;
            }
            return response.Result;
        }

        public override async Task<string> PostAsync()
        {
            Task<string> response = null;
            switch (authorizationType)
            {
                case AuthorizationType.Basic:
                    await (response = AuthorizationBasic.PostAsync());
                    break;

                case AuthorizationType.Token:
                    await (response = AuthorizationToken.PostAsync());
                    break;

                default:
                    break;
            }
            return response.Result;
        }

        protected override Task<string> RequestAsync(HttpRequestType httpRequestType)
        {
            throw new NotImplementedException();
        }
    }
}