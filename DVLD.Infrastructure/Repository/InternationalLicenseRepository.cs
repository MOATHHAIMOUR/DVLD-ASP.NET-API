using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class InternationalLicenseRepository : IInternationalLicenseRepository
    {
        private readonly IMapper _mapper;

        public InternationalLicenseRepository(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<bool> HasInternationalLicenseAsync(int driverId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand(InternationalLicenseStoredProcedures.SP_IsApplicantHasInternationalLicense, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    command.Parameters.Add(new SqlParameter("@DriverId", SqlDbType.Int) { Value = driverId });

                    // Add return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue,
                        SqlDbType = SqlDbType.Int
                    };

                    command.Parameters.Add(returnValue);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value
                    int result = (int)returnValue.Value;

                    // Return true if the driver has an international license (1), otherwise false (0)
                    return result == 1;
                }

            }
        }

        public async Task<(int applicationId, int internationalLicenseId)> AddInternationalLicenseAsync(string storedProcedure, InternationalLicense entity)
        {
            using var connection = new SqlConnection(DbSettings._connectionString);
            using var command = new SqlCommand(storedProcedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DriverId", entity.DriverId);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseId", entity.IssuedUsingLocalLicenseId);                                                                                          // Add more parameters as needed for the InternationalLicense properties
            command.Parameters.AddWithValue("@IssueDate", entity.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", entity.ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", entity.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserId", entity.CreatedByUserId);

            // Output parameters
            var applicationIdParam = new SqlParameter("@ApplicationId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var internationalLicenseIdParam = new SqlParameter("@InternationalLicenseId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(applicationIdParam);
            command.Parameters.Add(internationalLicenseIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            // Retrieve output parameter values
            int applicationId = (int)applicationIdParam.Value;
            int internationalLicenseId = (int)internationalLicenseIdParam.Value;

            return (applicationId, internationalLicenseId);

        }

        public async Task<IEnumerable<InternationalLicense>> GetAllAsync(string storedProcedure)
        {
            var licenses = new List<InternationalLicense>();

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
                            var license = _mapper.Map<IDataReader, InternationalLicense>(reader);

                            licenses.Add(license);
                        }
                    }
                }
            }

            return licenses;
        }

        public async Task<InternationalLicense?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            InternationalLicense? license = null;

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
                            license = _mapper.Map<IDataReader, InternationalLicense>(reader);
                        }
                    }
                }
            }

            return license;
        }

        public async Task<int> AddAsync(string storedProcedure, InternationalLicense entity)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ApplicationId", entity.ApplicationId);
                    command.Parameters.AddWithValue("@DriverId", entity.DriverId);
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseId", entity.IssuedUsingLocalLicenseId);
                    command.Parameters.AddWithValue("@IssueDate", entity.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", entity.ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserId", entity.CreatedByUserId);

                    // Execute and retrieve the new ID
                    var newId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(newId);
                }
            }
        }

        public async Task<bool> UpdateAsync(string storedProcedure, InternationalLicense entity)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@InternationalLicenseId", entity.InternationalLicenseId);
                    command.Parameters.AddWithValue("@ApplicationId", entity.ApplicationId);
                    command.Parameters.AddWithValue("@DriverId", entity.DriverId);
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseId", entity.IssuedUsingLocalLicenseId);
                    command.Parameters.AddWithValue("@IssueDate", entity.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", entity.ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserId", entity.CreatedByUserId);

                    // Execute the command and check the number of affected rows
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
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

    }
}