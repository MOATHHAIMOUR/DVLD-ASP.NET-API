namespace DVLD.Domain.IRepository
{
    public interface IJWTAuthenticationWithRefreshTokenRepository
    {
        public Task<bool> SaveRefreshTokenAsync(string loginId, string refreshToken, DateTime expirationDate, string createdByIp);

        public Task<(bool IsAuthenticated, bool IsActive)> AuthenticateUserAsync(string userId, string userPassword);
    }
}
