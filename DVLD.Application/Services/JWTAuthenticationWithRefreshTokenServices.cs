using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Common.settings;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites.Auth;
using DVLD.Domain.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DVLD.Application.Services
{
    public class JWTAuthenticationWithRefreshTokenServices : IJWTAuthenticationWithRefreshTokenServices
    {
        private readonly IJWTAuthenticationWithRefreshTokenRepository _jWTAuthenticationWithRefreshTokenRepository;
        private readonly JwtSettings _jwtSettings;

        public JWTAuthenticationWithRefreshTokenServices(IOptions<JwtSettings> jwtSettings, IJWTAuthenticationWithRefreshTokenRepository jWTAuthenticationWithRefreshTokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _jWTAuthenticationWithRefreshTokenRepository = jWTAuthenticationWithRefreshTokenRepository;
        }


        public async Task<Result<AuthenticationResponse>> AuthenticateUser(string userId, string password)
        {
            // Call the repository to authenticate the user
            (bool IsAuthenticated, bool IsActive) = await _jWTAuthenticationWithRefreshTokenRepository.AuthenticateUserAsync(userId, password);

            // Case 1: User is not authenticated (Invalid credentials)
            if (!IsAuthenticated)
            {
                return Result<AuthenticationResponse>.Failure(
                    Error.ValidationError("Authentication failed: Invalid credentials."));
            }

            // Case 2: User is authenticated but inactive
            if (IsAuthenticated && !IsActive)
            {
                return Result<AuthenticationResponse>.Failure(
                    Error.ValidationError("Authentication failed: Account is inactive. Please contact the administrator."));
            }

            // Case 3: User is authenticated and active (Success)
            if (IsAuthenticated && IsActive)
            {
                // Generate JWT Token
                string JwtToken = generateJWTToken(userId);

                // Generate Refresh Token

                (bool isSaved, string refreshToken) = await GenerateRefreshToken("", userId);

                if (!isSaved)
                    return Result<AuthenticationResponse>.Failure(
                        Error.ValidationError("An unexpected error occurred during authentication."));

                return Result<AuthenticationResponse>.Success(new AuthenticationResponse
                {
                    UserId = userId,
                    IsValid = true,
                    RefreshToken = refreshToken,
                    AuthenticationMessage = "Authentication Completed Successfully",
                    JWTToken = JwtToken,
                    ExpiersAt = DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes)

                });
            }

            // Fallback case (unexpected error)
            return Result<AuthenticationResponse>.Failure(
                Error.ValidationError("An unexpected error occurred during authentication."));
        }



        private async Task<(bool isSaved, string refreshToken)> GenerateRefreshToken(string ipAddr, string userId)
        {
            string refreshToken = GetRefreshToken();
            DateTime ExpirationDate = DateTime.Now.AddDays(7).Date;
            bool result = await _jWTAuthenticationWithRefreshTokenRepository.SaveRefreshTokenAsync(userId, refreshToken, ExpirationDate, ipAddr);

            return (result, refreshToken);
        }

        private string generateJWTToken(string userId)
        {
            string JWTToken = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, userId)]),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_jwtSettings.ExpirationMinutes))
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            JWTToken = tokenHandler.WriteToken(token);

            return JWTToken;

            /*
                One more approach, Iam commenting it for documentation and future reference:

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT")["SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserId),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(claims: claims, signingCredentials: credentials, expires: DateTime.UtcNow.AddMinutes(15));
                return new JwtSecurityTokenHandler().WriteToken(token);
             */
        }


        private string GetRefreshToken()
        {
            byte[] randomBytes = new byte[32]; // Define the size of the random byte array
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes); // Fill the byte array with cryptographically strong random values
            }
            return Convert.ToBase64String(randomBytes); // Convert to Base64 string
        }
    }
}
