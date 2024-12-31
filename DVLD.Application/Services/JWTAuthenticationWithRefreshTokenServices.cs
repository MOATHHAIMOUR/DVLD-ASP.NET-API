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
        private readonly IUserRepository _userRepository;
        public JWTAuthenticationWithRefreshTokenServices(IOptions<JwtSettings> jwtSettings, IJWTAuthenticationWithRefreshTokenRepository jWTAuthenticationWithRefreshTokenRepository, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _jWTAuthenticationWithRefreshTokenRepository = jWTAuthenticationWithRefreshTokenRepository;
            _userRepository = userRepository;
        }


        public async Task<Result<AuthenticationResponse>> AuthenticateUser(string Username, string password)
        {
            // Call the repository to authenticate the user
            (bool IsAuthenticated, bool IsActive) = await _jWTAuthenticationWithRefreshTokenRepository.AuthenticateUserAsync(Username, password);

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
                string JwtToken = generateJWTToken(Username);

                // Generate Refresh Token
                DateTime ExpirationDate = DateTime.Now.AddDays(7).Date;


                (bool isSaved, string refreshToken) = await GenerateRefreshToken(Username, ExpirationDate);

                if (!isSaved)
                    return Result<AuthenticationResponse>.Failure(
                        Error.ValidationError("An unexpected error occurred during authentication."));

                return Result<AuthenticationResponse>.Success(new AuthenticationResponse
                {
                    UserId = Username,
                    IsValid = true,
                    RefreshToken = refreshToken,
                    AuthenticationMessage = "Authentication Completed Successfully",
                    JWTToken = JwtToken,
                    tokenExpirersAt = DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                    refreshTokenExpiration = ExpirationDate

                });
            }

            // Fallback case (unexpected error)
            return Result<AuthenticationResponse>.Failure(
                Error.ValidationError("An unexpected error occurred during authentication."));
        }

        public async Task<Result<AuthenticationResponse>> RefreshTokenAsync(string token)
        {
            // Check if the refresh token is valid and retrieve the userId
            (int userId, DateTime? ExpierTime) = await _jWTAuthenticationWithRefreshTokenRepository.GetRefreshTokenDetailsAsync(token);

            if (userId == 0 || ExpierTime == null)
                return Result<AuthenticationResponse>.Failure(Error.ValidationError("The refresh token is not valid."));

            if (DateTime.Now > ExpierTime)
            {
                await _jWTAuthenticationWithRefreshTokenRepository.RevokeRefreshToken(token);
                return Result<AuthenticationResponse>.Failure(Error.ValidationError("The refresh token is not valid."));
            }

            // After confirming the refresh token is valid and not expier we can genrate new JWT 

            // 1. Revoke the old refresh token
            await _jWTAuthenticationWithRefreshTokenRepository.RevokeRefreshToken(token);

            // 2. Generate a new refresh token

            // get user data 
            var user = await _userRepository.GetUserByIdOrUserName(userId, null);

            DateTime refreshTokenExpirationDate = DateTime.Now.AddDays(7);

            (bool isSaved, string refreshToken) = await GenerateRefreshToken(user!.UserName, refreshTokenExpirationDate);

            if (!isSaved)
                return Result<AuthenticationResponse>.Failure(Error.ValidationError("Failed to generate a new refresh token."));

            // 3. Generate a new JWT token
            string newJwtToken = generateJWTToken(userId.ToString());

            return Result<AuthenticationResponse>.Success(new AuthenticationResponse
            {
                UserId = userId.ToString(),
                IsValid = true,
                RefreshToken = refreshToken,
                AuthenticationMessage = "Authentication completed successfully.",
                JWTToken = newJwtToken,
                tokenExpirersAt = DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                refreshTokenExpiration = refreshTokenExpirationDate
            });
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
        private async Task<(bool isSaved, string refreshToken)> GenerateRefreshToken(string username, DateTime ExpirationDate)
        {
            string refreshToken = GetRefreshToken();
            bool result = await _jWTAuthenticationWithRefreshTokenRepository.SaveRefreshTokenAsync(username, refreshToken, ExpirationDate, "");

            return (result, refreshToken);
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
