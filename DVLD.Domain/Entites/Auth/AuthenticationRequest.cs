namespace DVLD.Domain.Entites.Auth
{
    public class AuthenticationRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Operation { get; set; }
    }
}
