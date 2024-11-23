using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<int> AddUserAsycn(User user)
        {
            int insertedUserId = -1;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP__AddUser", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@PersonId", user.PersonId);
                    cmd.Parameters.AddWithValue("@Username", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                    // Open connection and execute the command
                    await connection.OpenAsync();

                    insertedUserId = Convert.ToInt32(await cmd.ExecuteScalarAsync());



                }
            }

            return insertedUserId;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_DeleteUserById", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter for PersonId
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    // Execute the command
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected > 0;
        }

        public async Task<User?> GetUserByIdOrUserName(int? UserId, string? UserName)
        {
            User? user = null;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserName", UserName);


                    await connection.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                            };
                        }
                    }
                }
            }

            return user;
        }

        public async Task<bool> UpdateUserAsycn(User user)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var cmd = new SqlCommand("SP_UpdateUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Map Person object properties to stored procedure parameters
                    cmd.Parameters.AddWithValue("@UserID", user.UserId);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);


                    // Open the connection and execute the command
                    await connection.OpenAsync();
                    affectedRows = await cmd.ExecuteNonQueryAsync();
                }
            }

            return affectedRows > 0;
        }

        public async Task<List<User>> GetUsers(int? userId = null, int? personId = null, string? userName = null, bool? isActive = null, string? sortBy = null, EnumSortDirection? sortDirection = EnumSortDirection.ASC, int? pageSize = 10, int? pageNumber = 1)
        {
            List<User> users = [];

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_GetUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@UserId", userId.HasValue ? (object)userId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@PersonId", personId.HasValue ? (object)personId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@UserName", userName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", isActive.HasValue ? (object)isActive.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SortBy", sortBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SortDirection", sortDirection.HasValue ? sortDirection.Value == EnumSortDirection.ASC ? "ASC" : "DESC" : DBNull.Value);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            });
                        }
                    }
                }
            }

            return users;
        }


    }
}
