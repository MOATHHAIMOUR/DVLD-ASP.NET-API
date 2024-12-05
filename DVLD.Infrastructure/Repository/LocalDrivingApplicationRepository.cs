using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class LocalDrivingApplicationRepository : GenericRepository<LocalDrivingLicenseApplication>, ILocalDrivingApplicationRepository
    {
        public LocalDrivingApplicationRepository(IMapper mapper) : base(mapper)
        {
        }

        public async Task<(int ApplicationId, int RenewLicenseId)> RenewLocalDrivingLicenseAsync(
           int licenseId,
           int createdByUserId,
           DateTime expirationDate)
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



        public async Task<(int ApplicationId, int ReplacementForLostLicenseId)> ReplaceForLostLocalDrivingLicenseAsync(
            int licenseId,
            int createdByUserId,
            DateTime expirationDate)
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


        public async Task<int> DetainLocalDrivingLicenseAsync(
        int licenseId,
        DateTime detainDate,
        decimal fineFees,
        int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DetainLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@DetainDate", detainDate);
                    command.Parameters.AddWithValue("@FineFees", fineFees);
                    command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);

                    // Add output parameter
                    SqlParameter detainIdParam = new SqlParameter("@DetainId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(detainIdParam);

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute the command asynchronously
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output value
                    int detainId = detainIdParam.Value != DBNull.Value ? (int)detainIdParam.Value : 0;

                    return (detainId);
                }
            }
        }


        public async Task<int> ReleaseDetainLocalDrivingLicenseAsync(
         int licenseId,
         DateTime detainDate,
         decimal fineFees,
         int releasedByUserId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ReleaseDetainLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@DetainDate", detainDate);
                    command.Parameters.AddWithValue("@FineFees", fineFees);
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


    }
}