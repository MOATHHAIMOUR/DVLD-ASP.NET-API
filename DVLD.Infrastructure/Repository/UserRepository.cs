using AutoMapper;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(string storedProcedure, User entity)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to match the stored procedure
                    command.Parameters.AddWithValue("@UserId", entity.UserId);
                    command.Parameters.AddWithValue("@PersonId", entity.PersonId);
                    command.Parameters.AddWithValue("@UserName", entity.UserName);
                    command.Parameters.AddWithValue("@Password", (object?)entity.Password ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", entity.IsActive);

                    // Open the connection
                    await connection.OpenAsync();

                    // Execute the command and get the result
                    int result = await command.ExecuteNonQueryAsync();

                    // Return the result
                    return result;
                }
            }
        }


        public async Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter to match the stored procedure
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    // Open the connection
                    await connection.OpenAsync();

                    // Execute the command and check the number of rows affected
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was affected
                    return rowsAffected > 0;
                }
            }
        }

        public Task<IEnumerable<User>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetFilterdUsersAsync(string storedProcedure, UsersSearchParameters usersSearchParameters)
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters dynamically based on UsersSearchParameters
                    command.Parameters.AddWithValue("@UserId", usersSearchParameters.UserId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@UserName", usersSearchParameters.UserName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", usersSearchParameters.IsActive ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SortBy", usersSearchParameters.SortBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SortDirection", usersSearchParameters.SortDirection ?? "ASC");
                    command.Parameters.AddWithValue("@PageSize", usersSearchParameters.PageSize ?? 10);
                    command.Parameters.AddWithValue("@PageNumber", usersSearchParameters.PageNumber ?? 1);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var user = _mapper.Map<IDataReader, User>(reader);

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
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

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
