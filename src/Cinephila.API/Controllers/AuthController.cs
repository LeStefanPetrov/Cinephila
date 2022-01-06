using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private const string ClientID = "21758989588-o99527rg1tidhva82aigfg1u6ku81b6q.apps.googleusercontent.com";
        private const string ClientSecret = "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";

        [HttpPost]
        public async Task<ActionResult> GoogleAuth()
        {
            string[] scopes = { GmailService.Scope.GmailReadonly , Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.UserinfoEmail };

            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = ClientID,
                    ClientSecret = ClientSecret,
                }, scopes, "user", CancellationToken.None).Result;

            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });

            var oauthSerivce = new Oauth2Service( new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "OAuth 2.0 Sample",
            });


            var profile = await oauthSerivce.Userinfo.Get().ExecuteAsync();

            var info =  service.Users.GetProfile("framedout98@gmail.com").Execute();

            return Ok(info);
        }
    }
}
