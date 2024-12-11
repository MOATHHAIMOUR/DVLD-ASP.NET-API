using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class LocalDrivingApplicationRepository : ILocalDrivingApplicationRepository
    {
        private readonly IMapper _mapper;

        public LocalDrivingApplicationRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<(int ApplicationId, int RenewLicenseId)> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RenewLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);
                    command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

                    // Add output parameters
                    SqlParameter applicationIdParam = new SqlParameter("@ApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(applicationIdParam);

                    SqlParameter renewLicenseIdParam = new SqlParameter("@RenewLicenseId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(renewLicenseIdParam);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output values
                    int applicationId = applicationIdParam.Value != DBNull.Value ? (int)applicationIdParam.Value : 0;
                    int renewLicenseId = renewLicenseIdParam.Value != DBNull.Value ? (int)renewLicenseIdParam.Value : 0;

                    return (applicationId, renewLicenseId);
                }
            }
        }

        public async Task<(int ApplicationId, int ReplacementDamageForLicenseId)> ReplaceForDamageLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ReplaceForDamageLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);
                    command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

                    // Add output parameters
                    SqlParameter applicationIdParam = new SqlParameter("@ApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(applicationIdParam);

                    SqlParameter replacementDamageForLicenseIdParam = new SqlParameter("@ReplacementDamageForLicenseId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(replacementDamageForLicenseIdParam);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output values
                    int applicationId = applicationIdParam.Value != DBNull.Value ? (int)applicationIdParam.Value : 0;
                    int replacementDamageForLicenseId = replacementDamageForLicenseIdParam.Value != DBNull.Value ? (int)replacementDamageForLicenseIdParam.Value : 0;

                    return (applicationId, replacementDamageForLicenseId);
                }
            }
        }

        public async Task<(int ApplicationId, int ReplacementForLostLicenseId)> ReplaceForLostLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ReplaceForLostLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);
                    command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

                    // Add output parameters
                    SqlParameter applicationIdParam = new SqlParameter("@ApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(applicationIdParam);

                    SqlParameter replacementForLostLicenseIdParam = new SqlParameter("@ReplacementForLostLicenseId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(replacementForLostLicenseIdParam);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output values
                    int applicationId = applicationIdParam.Value != DBNull.Value ? (int)applicationIdParam.Value : 0;
                    int replacementForLostLicenseId = replacementForLostLicenseIdParam.Value != DBNull.Value ? (int)replacementForLostLicenseIdParam.Value : 0;

                    return (applicationId, replacementForLostLicenseId);
                }
            }
        }

        public async Task<bool> IsLicenseDetainedAsync(int licenseId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsLicenseDetain", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    command.Parameters.AddWithValue("@LicenseId", licenseId);

                    // Add return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Open the connection asynchronously
                    await connection.OpenAsync();

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value
                    int result = (int)returnValue.Value;

                    // Return true if the license is detained, false otherwise
                    return result == 1;
                }
            }
        }

        public async Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsLocalDrivingLicenseExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    command.Parameters.AddWithValue("@LicenseId", licenseId);

                    // Add return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Open the connection asynchronously
                    await connection.OpenAsync();

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value
                    int result = (int)returnValue.Value;

                    // Return true if the license exists, false otherwise
                    return result == 1;
                }
            }
        }

        public Task<IEnumerable<LocalDrivingLicenseApplication>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<LocalDrivingLicenseApplication?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(string storedProcedure, LocalDrivingLicenseApplication entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, LocalDrivingLicenseApplication entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }
    }
}