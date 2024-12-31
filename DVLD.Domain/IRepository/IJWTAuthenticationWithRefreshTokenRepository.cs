namespace DVLD.Domain.IRepository
{
    public interface IJWTAuthenticationWithRefreshTokenRepository
    {
        public Task<bool> SaveRefreshTokenAsync(string loginId, string refreshToken, DateTime expirationDate, string createdByIp);
        public Task<(int UserId, DateTime? ExpireTime)> GetRefreshTokenDetailsAsync(string token);
        public Task<bool> RevokeRefreshToken(string token);
        public Task<(bool IsAuthenticated, bool IsActive)> AuthenticateUserAsync(string userId, string userPassword);
    }
}
