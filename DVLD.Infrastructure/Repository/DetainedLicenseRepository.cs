using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class DetainedLicenseRepository : IDetainedLicenseRepository
    {
        private readonly IMapper _mapper;

        public DetainedLicenseRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(string storedProcedure, DetainedLicense entity)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@LicenseId", entity.LicenseId);
                    command.Parameters.AddWithValue("@DetainDate", entity.DetainDate);
                    command.Parameters.AddWithValue("@FineFees", entity.FineFees ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedByUserId", entity.CreatedByUserId);
                    command.Parameters.AddWithValue("@IsReleased", entity.IsReleased);
                    command.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ReleasedByUserId", entity.ReleasedByUserId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ReleaseApplicationId", entity.ReleaseApplicationId ?? (object)DBNull.Value);

                    // Execute and retrieve the new DetainId
                    var newId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(newId);
                }
            }
        }


        public async Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    // Execute the command and check the number of affected rows
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }


        public async Task<IEnumerable<DetainedLicense>> GetAllAsync(string storedProcedure)
        {
            var licenses = new List<DetainedLicense>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            licenses.Add(_mapper.Map<IDataReader, DetainedLicense>(reader));
                        }
                    }
                }
            }

            return licenses;
        }


        public async Task<DetainedLicense?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            DetainedLicense? license = null;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            license = new DetainedLicense
                            {
                                DetainId = reader.GetInt32(reader.GetOrdinal("DetainId")),
                                LicenseId = reader.GetInt32(reader.GetOrdinal("LicenseId")),
                                DetainDate = reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                                FineFees = reader.IsDBNull(reader.GetOrdinal("FineFees")) ? null : reader.GetDecimal(reader.GetOrdinal("FineFees")),
                                CreatedByUserId = reader.GetInt32(reader.GetOrdinal("CreatedByUserId")),
                                IsReleased = reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                                ReleaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                ReleasedByUserId = reader.IsDBNull(reader.GetOrdinal("ReleasedByUserId")) ? null : reader.GetInt32(reader.GetOrdinal("ReleasedByUserId")),
                                ReleaseApplicationId = reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationId")) ? null : reader.GetInt32(reader.GetOrdinal("ReleaseApplicationId")),
                            };
                        }
                    }
                }
            }

            return license;
        }


        public async Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    // Execute the command and retrieve the result
                    var result = await command.ExecuteScalarAsync();
                    return result != null && Convert.ToInt32(result) > 0;
                }
            }
        }


        public async Task<int> ReleaseDetainLocalDrivingLicenseAsync(int licenseId, DateTime detainDate, int releasedByUserId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ReleaseDetainLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@DetainDate", detainDate);
                    command.Parameters.AddWithValue("@ReleasedByUserId", releasedByUserId);

                    // Add output parameter
                    SqlParameter releaseApplicationIdParam = new SqlParameter("@ReleaseApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(releaseApplicationIdParam);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output value
                    int releaseApplicationId = releaseApplicationIdParam.Value != DBNull.Value ? (int)releaseApplicationIdParam.Value : 0;

                    return releaseApplicationId;
                }
            }
        }

        public Task<bool> UpdateAsync(string storedProcedure, DetainedLicense entity)
        {
            throw new NotImplementedException();
        }
    }
}
