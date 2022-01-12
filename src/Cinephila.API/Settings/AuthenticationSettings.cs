namespace Cinephila.API.Settings
{
    public class AuthenticationSettings
    {
        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string[] Scopes { get; set; }

        public string Audience { get; set; }
    }
}
