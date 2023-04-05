using Newtonsoft.Json.Schema.Generation;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace BurqAuthRestSharp.Magento
{
    public class HttpAuth : AuthorizationBase
    {
        private AuthorizationBasic AuthorizationBasic;
        private AuthorizationNoAuth AuthorizationNoAuth;
        private AuthorizationToken AuthorizationToken;
        private AuthorizationType authorizationType;


        public HttpAuth(AuthorizationBasic authorizationBasic) : base(authorizationBasic.URL)
        {
            AuthorizationBasic = authorizationBasic;
            authorizationType = AuthorizationType.Basic;
        }
        public HttpAuth(AuthorizationToken authorizationToken) : base(authorizationToken.URL)
        {
            AuthorizationToken = authorizationToken;
            authorizationType = AuthorizationType.Token;
        }

        public HttpAuth(AuthorizationNoAuth authorizationNoAuth) : base(authorizationNoAuth.URL)
        {
            AuthorizationNoAuth = authorizationNoAuth;
            authorizationType = AuthorizationType.None;
        }

        public static string GetMetaData()
        {
            return new JSchemaGenerator().Generate(typeof(MagentoAuth)).ToString();
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

        protected override Task<string> RequestAsync(Method restMethod)
        {
            throw new NotImplementedException();
        }

    }
}