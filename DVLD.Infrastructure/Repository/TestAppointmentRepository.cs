using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class TestAppointmentRepository : ITestAppointmentRepository
    {
        private readonly IMapper _mapper;

        public TestAppointmentRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> IsApplicantPassTestAsync(int localDrivingApplicationId, int testTypeId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsApplicantPassTest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@LocalDrivingApplicationId", localDrivingApplicationId);
                    command.Parameters.AddWithValue("@TestTypeId", testTypeId);

                    // Add a return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Open connection asynchronously and execute
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Return true if the stored procedure returns 1, otherwise false
                    return (int)returnValue.Value == 1;
                }
            }
        }

        public async Task<bool> IsApplicantHasAppointmentTestNotLockedAsync(int localDrivingApplicationId, int testTypeId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsApplicantHasAppointmentTestNotLocked", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@LocalDrivingApplicationId", localDrivingApplicationId);
                    command.Parameters.AddWithValue("@TestTypeId", testTypeId);

                    // Add a return value parameter
                    SqlParameter returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Open connection asynchronously and execute
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Return true if the stored procedure returns 1, otherwise false
                    return (int)returnValue.Value == 1;
                }
            }

        }

        public Task<IEnumerable<TestAppointment>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<TestAppointment?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(string storedProcedure, TestAppointment entity)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", entity.LocalDrivingLicenseApplicationId);
                    command.Parameters.AddWithValue("@TestTypeId", entity.TestTypeId);
                    command.Parameters.AddWithValue("@CreatedByUserID", entity.CreatedByUserId);

                    // Output parameter to get the newly created TestAppointmentId
                    var outputParam = new SqlParameter("@RetakeTestApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    var outputParam2 = new SqlParameter("@TestApplicationId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam2);

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the TestAppointmentId from the output parameter
                    return (int)outputParam2.Value;
                }
            }
        }

        public Task<bool> UpdateAsync(string storedProcedure, TestAppointment entity)
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