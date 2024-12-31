using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class JWTAuthenticationWithRefreshTokenRepository : IJWTAuthenticationWithRefreshTokenRepository
    {
        public async Task<(bool IsAuthenticated, bool IsActive)> AuthenticateUserAsync(string UserName, string userPassword)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SP_AuthenticateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    command.Parameters.AddWithValue("@UserName", UserName);
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

        public async Task<bool> SaveRefreshTokenAsync(string UserId, string refreshToken, DateTime expirationDate, string createdByIp)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SaveRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@UserName", UserId);
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

        public async Task<bool> RevokeRefreshToken(string token)
        {

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP_RevokeRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the input parameter
                    command.Parameters.Add(new SqlParameter("@RefreshToken", SqlDbType.NVarChar, 100)
                    {
                        Value = token
                    });

                    // Execute the procedure
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // If rows are affected, the operation was successful
                    return rowsAffected > 0;
                }
            }

        }

        public async Task<(int UserId, DateTime? ExpireTime)> GetRefreshTokenDetailsAsync(string token)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SP_GetUserIdLinkedToRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    command.Parameters.Add(new SqlParameter("@RefreshToken", SqlDbType.NVarChar, 100)
                    {
                        Value = token
                    });

                    // Add output parameters
                    var userIdParam = new SqlParameter("@UserId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userIdParam);

                    var expireTimeParam = new SqlParameter("@ExpireTime", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(expireTimeParam);

                    // Execute the procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output values
                    int userId = (int)userIdParam.Value;
                    DateTime? expireTime = expireTimeParam.Value == DBNull.Value ? (DateTime?)null : (DateTime)expireTimeParam.Value;

                    return (userId, expireTime);
                }
            }
        }

    }
}
