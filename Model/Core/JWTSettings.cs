namespace BasketballAcademy.Model.Core
{
    public class JWTSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int ExpiryMinute { get; set; }
        public string Key { get; set; }
    }
}
