using BurqAuthRestSharp;
using BurqAuthRestSharp.Magento;
using BurqAuthRestSharp.Shopify;
using BurqAuthRestSharp.WooCommerce;
using System;
using System.Threading.Tasks;

namespace BurqAuth
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //string URL = "https://pbfywebtest.wpengine.com/wp-json/wc/v2/customers";
            //string UserName = "ck_cae86d5b52670031b51c447e816a7ddd32e387af";
            //string Password = "cs_f72f1f001bcecab45e6eb0c99f21688c78e4babb";
            //
            //string json = WooCommerceAuth.GetMetaData();
            //Console.WriteLine(json);
            //
            //WooCommerceAuth wooCommerceAuth = new WooCommerceAuth(URL, UserName, Password);
            //var task = wooCommerceAuth.GetAsync();
            //Console.WriteLine(task.Result);


            //AuthorizationRest authorizationRest = new AuthorizationRest(authorizationBasic);
            //var task2 = authorizationRest.GetAsync();
            //Console.WriteLine(task2.Result);

            //string URL = "https://integration3-b5sbmty-aat4wddnrsnrg.us-5.magentosite.cloud/rest/V1/integration/admin/token";
            //
            //string json = MagentoAuth.GetMetaData();
            //Console.WriteLine(json);
            //
            //MagentoAuth magentoAuth= new MagentoAuth(URL,"danyal.manzoor","D9WsUP24Cx9g4VJI@@");
            //var task3 = magentoAuth.PostAsync();
            //Console.WriteLine(task3.Result);


            string json = ShopifyAuth.GetMetaData();
            Console.WriteLine(json);

            ShopifyAuth shopifyAuth = new ShopifyAuth("nabeel-dev", "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");
            var task3 = shopifyAuth.GetAsync();
            Console.WriteLine(task3.Result);


            //
            //URL = "https://integration3-b5sbmty-aat4wddnrsnrg.us-5.magentosite.cloud/rest/V1/customers/1";
            //authorizationNoAuth.URL = URL;
            //
            //AuthorizationToken authorizationToken = new AuthorizationToken(URL, task3.Result.ToString());
            //var task4 = authorizationToken.GetAsync();
            //Console.WriteLine(task4.Result);
            //
            //AuthorizationRest authorizationRest = new AuthorizationRest(authorizationToken);
            //var task5 = authorizationRest.GetAsync();
            //Console.WriteLine(task5?.Result);

            //string AuthURL = "https://github.com/login/oauth/authorize?client_id=Iv1.8a53adb59e6559be&scope=RANDOM&scope=project";
            //string accessTokenUrl = "https://github.com/login/oauth/access_token";
            //string refershTokenUrl = "https://github.com/login/oauth/refresh_token";
            //string clinetId = "Iv1.8a53adb59e6559be";
            //string clinetSecret = "936d9fe553290e9cfec82f2a7d08902ad2d1f868";
            //string callbackURl = "https://localhost/callback";
            //string code = "cc4b5801a15db7c48641";
            //if (code == string.Empty)
            //{
            //   // Open authorization request URL in default browser
            //   Process.Start(new ProcessStartInfo
            //   {
            //       FileName = AuthURL,
            //       UseShellExecute = true
            //   });
            //}
            //else
            //{
            //   AuthorizationOAuth authorizationOAuth = new AuthorizationOAuth(clinetId, clinetSecret, code, accessTokenUrl, refershTokenUrl);
            //   authorizationOAuth.PostAsync();
            //}
        }
    }
}