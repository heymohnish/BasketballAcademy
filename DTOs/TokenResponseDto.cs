﻿namespace BasketballAcademy.DTOs
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
