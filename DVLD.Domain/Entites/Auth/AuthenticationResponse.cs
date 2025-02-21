﻿namespace DVLD.Domain.Entites.Auth
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AuthenticationMessage { get; set; }
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime tokenExpirersAt { set; get; }
        public DateTime refreshTokenExpiration { get; set; }
        public bool IsValid { get; set; }
    }
}
