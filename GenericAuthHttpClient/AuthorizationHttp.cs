using System;
using System.Threading.Tasks;

namespace BurqAuthHttpClient
{
    public class AuthorizationHttp : AuthorizationBase
    {
        private AuthorizationNoAuth AuthorizationNoAuth;
        private AuthorizationBasic AuthorizationBasic;
        private AuthorizationToken AuthorizationToken;
        private AuthorizationType authorizationType;

        public AuthorizationHttp(AuthorizationNoAuth authorizationNoAuth) : base(authorizationNoAuth.URL)
        {
            authorizationType = AuthorizationType.None;
            AuthorizationNoAuth = authorizationNoAuth;
        }

        public AuthorizationHttp(AuthorizationBasic authorizationBasic) : base(authorizationBasic.URL)
        {
            authorizationType = AuthorizationType.Basic;
            this.AuthorizationBasic = authorizationBasic;
        }

        public AuthorizationHttp(AuthorizationToken authorizationToken) : base(authorizationToken.URL)
        {
            authorizationType = AuthorizationType.Token;
            this.AuthorizationToken = authorizationToken;
        }

        public override async Task<string> GetAsync()
        {
            Task<string> response = null;
            switch (authorizationType)
            {
                case AuthorizationType.None:
                    await (response = AuthorizationNoAuth.GetAsync());
                    break;

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
                case AuthorizationType.None:
                    await (response = AuthorizationNoAuth.PostAsync());
                    break;

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