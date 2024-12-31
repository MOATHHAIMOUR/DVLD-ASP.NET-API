using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
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

        public async Task<bool> IsApplicantHasAcActiveApplicationPerApplicationType(int personId, int applicationTypeId)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                var command = new SqlCommand("SP_IsApplicantHasAcActiveApplicationPerApplicationType", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add input parameters
                command.Parameters.AddWithValue("@ApplicantPersonId", personId);
                command.Parameters.AddWithValue("@ApplicationTypeId", applicationTypeId);

                // Add the return value parameter
                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                // Open the connection
                await connection.OpenAsync();

                // Execute the command
                await command.ExecuteNonQueryAsync();

                // Retrieve the return value
                var result = (int)returnValue.Value;

                // Convert the result to a boolean and return it
                return result == 1;
            }
        }



        public async Task<bool> IsApplicantHasAcActiveLocalDrivingApplication(int personId, int LicenseClassId)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                var command = new SqlCommand("SP_IsApplicantHasAcActiveLocalDrivingApplication", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add input parameters
                command.Parameters.AddWithValue("@ApplicantPersonId", personId);
                command.Parameters.AddWithValue("@LicenseClassId", LicenseClassId);

                // Add the return value parameter
                var returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);

                // Open the connection
                await connection.OpenAsync();

                // Execute the command
                await command.ExecuteNonQueryAsync();

                // Retrieve the return value
                var result = (int)returnValue.Value;

                // Convert the result to a boolean and return it
                return result == 1;
            }
        }


        public async Task<bool> IsApplicantHasLocalLicenseAsync(int licenseClassId, int applicationId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsApplicantHasLocalLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@LicenseClassId", SqlDbType.Int) { Value = licenseClassId });
                    command.Parameters.Add(new SqlParameter("@ApplicationId", SqlDbType.Int) { Value = applicationId });

                    // Add a return parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    await connection.OpenAsync();

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value
                    int result = (int)returnValue.Value;
                    return result == 1;
                }
            }
        }

        public async Task<bool> IsApplicanHasAlreadyActiveLicenseWithSameType(int personId, int licenseClassId)
        {
            const string storedProcedureName = "SP_IsApplicanHasAlreadyLicenseWithSameType";

            using (var connection = new SqlConnection(DbSettings._connectionString)) // Replace _yourConnectionString with your actual connection string
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@applicantPersonId", personId);
                    command.Parameters.AddWithValue("@LicenseclassId", licenseClassId);

                    // Add the return value parameter
                    var returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Get the return value
                    var result = (int)returnValue.Value;

                    // Convert the result to a boolean and return it
                    return result == 1;
                }
            }
        }

        public async Task<bool> IsLocalDrivingApplicationCompeletedOrCancelled(int localDrivingApplicationId)
        {

            using (var connection = new SqlConnection(DbSettings._connectionString))
            using (var command = new SqlCommand(LocalDrivingApplicationStoredProcedures.SP_IsLocalDrivingApplicationCompeletedOrCancelled, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LocalDrivingApplicationId", SqlDbType.Int)
                {
                    Value = localDrivingApplicationId
                });

                var returnParameter = new SqlParameter
                {
                    ParameterName = "@ReturnVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue
                };

                command.Parameters.Add(returnParameter);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                int result = (int)returnParameter.Value;

                return result == 0; // true if completed or cancelled, false otherwise
            }
        }

        public async Task<(int ApplicationId, int RenewLicenseId)> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RenewLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    command.Parameters.AddWithValue("@LicenseId", licenseId);
                    command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);

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

        public async Task<int> AddAsync(string storedProcedure, LocalDrivingLicenseApplication entity)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@ApplicantPersonId", entity.Application.ApplicantPersonId);
                    command.Parameters.AddWithValue("@CreatedByUserId", entity.Application.CreatedByUserId);
                    command.Parameters.AddWithValue("@LicenseClassId", entity.LicenseClassId);

                    // Open connection
                    await connection.OpenAsync();

                    // Execute the command
                    object result = await command.ExecuteScalarAsync();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
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

        public async Task<bool> IsLocalDrivingApplicationCompletedAsync(int localDrivingApplication)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SP_IsLocalDrivingApplicationCompleted", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@localDrivingApplication", localDrivingApplication);

                    // Output parameter to capture the return value
                    var returnValue = new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Execute the procedure
                    await command.ExecuteNonQueryAsync();

                    // Return true if the stored procedure returned 1
                    return (int)returnValue.Value == 1;
                }
            }
        }

        public async Task<bool> CancelLocalDrivingApplication(int localDrivingApplication)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_CancelLocalDrivingApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@localDrivingApplication", SqlDbType.Int)
                    {
                        Value = localDrivingApplication
                    });

                    await connection.OpenAsync();

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Assuming the procedure always updates one row if successful
                    return rowsAffected > 0;
                }
            }
        }
    }
}