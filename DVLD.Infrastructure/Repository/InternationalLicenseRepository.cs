using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
using DVLD.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class InternationalLicenseRepository : GenericRepository<InternationalLicense>, IInternationalLicenseRepository
    {
        public InternationalLicenseRepository(IMapper mapper) : base(mapper)
        {
        }

        public async Task<(bool IsValid, int LicenseId)> CheckDriverHasOrdinaryLocalDrivingLicenseAsync(int driverId)
        {
            // Output variables
            int licenseId = 0;
            bool isValid = false;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_IsDriverHasOrdinaryLocalDrivingLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    command.Parameters.AddWithValue("@DriverId", driverId);

                    // Output parameters
                    var licenseIdParam = new SqlParameter("@LicenseId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var isValidParam = new SqlParameter("@IsValid", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(licenseIdParam);
                    command.Parameters.Add(isValidParam);

                    // Open connection
                    await connection.OpenAsync();

                    // Execute stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve output parameter values
                    licenseId = (int)licenseIdParam.Value;
                    isValid = (bool)isValidParam.Value;
                }
            }

            return (isValid, licenseId);
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
    }
}