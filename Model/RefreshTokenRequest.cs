namespace BasketballAcademy.Model
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class UserRefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string UserName { get; set; }
    }
}
