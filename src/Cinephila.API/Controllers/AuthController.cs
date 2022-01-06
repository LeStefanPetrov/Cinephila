using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult GoogleAuth()
        {
            string[] scopes = { "https://www.googleapis.com/auth/gmail.readonly" };

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

            var profile = service.Users.GetProfile("framedout98@gmail.com").Execute();

            return Ok(credentials);
        }
    }
}
