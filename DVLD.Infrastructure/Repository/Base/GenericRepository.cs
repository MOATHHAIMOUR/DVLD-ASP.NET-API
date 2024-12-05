using AutoMapper;
using DVLD.Domain.IRepository.Base;
using DVLD.Infrastructure.InfrastructHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly string _connectionString = DbSettings._connectionString;
        private readonly IMapper _mapper;

        public GenericRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure, object? paramObject = null)
        {
            var results = new List<TEntity>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Map parameters from DTO
                if (paramObject != null)
                {
                    var parameters = Helpers.CreateSqlParametersFromObject(paramObject);
                    command.Parameters.AddRange(parameters);
                }

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var entity = _mapper.Map<TEntity>(reader);
                        results.Add(entity);
                    }
                }
            }

            return results;
        }
        public virtual async Task<IEnumerable<View>> GetAllAsync<View>(string storedProcedure, object? paramObject = null)
        {
            var results = new List<View>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Map parameters from DTO
                if (paramObject != null)
                {
                    foreach (var property in paramObject.GetType().GetProperties())
                    {
                        var parameterName = $"@{property.Name}";
                        var parameterValue = property.GetValue(paramObject);
                        if (Helpers.IsDefaultOrNull(parameterValue, property))
                        {
                            command.Parameters.AddWithValue(parameterName, DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(parameterName, parameterValue);
                        }
                    }
                }

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var entity = _mapper.Map<View>(reader);
                        results.Add(entity);
                    }
                }
            }

            return results;
        }
        public virtual async Task<TEntity?> GetByIdAsync(string storedProcedure, string propertyName, int value)
        {
            TEntity? result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the `@Id` parameter
                command.Parameters.AddWithValue($"@{propertyName}", value);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Map the data to the entity
                        result = _mapper.Map<TEntity>(reader);
                    }
                }
            }

            return result;
        }
        public virtual async Task<View?> GetByIdAsync<View>(string storedProcedure, string propertyName, int value)
        {
            View? result = default;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the `@Id` parameter
                command.Parameters.AddWithValue($"@{propertyName}", value);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Map the data to the entity
                        result = _mapper.Map<View>(reader);
                    }
                }
            }

            return result;
        }
        public virtual async Task<int> AddAsync(string storedProcedure, TEntity entity, object? IncluedPropertyInSqlPrameter)
        {
            int insertedId = -1;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using var command = new SqlCommand(storedProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Generate SQL parameters from the entity and add them to the command
                    var parameters = Helpers.CreateSqlParametersFromObject(entity, IncluedPropertyInSqlPrameter);

                    command.Parameters.AddRange(parameters);

                    await connection.OpenAsync();
                    // Execute the stored procedure and retrieve the inserted ID
                    insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            catch (Exception err)
            {

            }


            return insertedId;
        }
        public virtual async Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            int numberOfRowsAffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the @Id parameter
                command.Parameters.AddWithValue($"@{propertyName}", value);

                await connection.OpenAsync();
                numberOfRowsAffected = await command.ExecuteNonQueryAsync(); // Execute the delete operation
            }

            return numberOfRowsAffected > 0;
        }
        public virtual async Task<bool> UpdateAsync(string storedProcedure, TEntity entity)
        {
            int numberOfRowsAffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Map entity properties to SQL parameters
                foreach (var property in typeof(TEntity).GetProperties())
                {
                    var value = property.GetValue(entity) ?? DBNull.Value; // Handle null values
                    command.Parameters.AddWithValue($"@{property.Name}", value);
                }

                await connection.OpenAsync();
                numberOfRowsAffected = await command.ExecuteNonQueryAsync(); // Execute the stored procedure
            }

            return numberOfRowsAffected > 0;
        }



    }
}