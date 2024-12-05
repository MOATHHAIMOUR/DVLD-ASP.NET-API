using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class TestAppointmentRepository : GenericRepository<TestAppointment>, ITestAppointmentRepository
    {
        public TestAppointmentRepository(IMapper mapper) : base(mapper)
        {
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
    }
}