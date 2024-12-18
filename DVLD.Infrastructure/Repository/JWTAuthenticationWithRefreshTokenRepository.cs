using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class JWTAuthenticationWithRefreshTokenRepository : IJWTAuthenticationWithRefreshTokenRepository
    {
        public async Task<(bool IsAuthenticated, bool IsActive)> AuthenticateUserAsync(string userId, string userPassword)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SP_AuthenticateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@UserPassword", userPassword);

                    // Output Parameters
                    var isAuthenticatedParam = new SqlParameter("@IsAuthenticated", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(isAuthenticatedParam);

                    var isActiveParam = new SqlParameter("@IsActive", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(isActiveParam);



                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve Output Parameters
                    bool isAuthenticated = (bool)isAuthenticatedParam.Value;
                    bool isActive = (bool)isActiveParam.Value;


                    return (isAuthenticated, isActive);
                }
            }
        }

        public async Task<bool> SaveRefreshTokenAsync(string loginId, string refreshToken, DateTime expirationDate, string createdByIp)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SaveRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LoginId", loginId);
                    command.Parameters.AddWithValue("@RefreshToken", refreshToken);
                    command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                    command.Parameters.AddWithValue("@CreatedByIp", createdByIp);

                    // Add return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = SqlDbType.Bit, // Return value type matches @OperationSuccess (BIT)
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value (0 for failure, 1 for success)
                    return Convert.ToBoolean(returnValue.Value);
                }
            }
        }
    }
}
