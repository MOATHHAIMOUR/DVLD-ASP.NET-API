using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly IMapper _mapper;

        public TestRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(string storedProcedure, Test entity)
        {
            int insertedId = -1;
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@TestAppointmentId", SqlDbType.Int) { Value = entity.TestAppointmentId });
                    command.Parameters.Add(new SqlParameter("@TestResult", SqlDbType.Bit) { Value = entity.TestResult });
                    command.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar, 500) { Value = (object?)entity.Notes ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@CreatedByUserId", SqlDbType.Int) { Value = entity.CreatedByUserId });

                    // Execute the command and return the newly inserted ID
                    object? result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        insertedId = id;
                    }
                }
            }
            return insertedId;
        }

        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Test>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<Test?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsApplicantPassAllTests(string storedProcedure, int applicationId)
        {
            // Define the result variable to store the output of the stored procedure
            int result;

            // Assuming you have a valid connection string in your configuration
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                // Open the SQL connection
                await connection.OpenAsync();

                using (var command = new SqlCommand("SP_IsApplicantPassAllTests", connection))
                {
                    // Set the command type to StoredProcedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the input parameter for the stored procedure
                    command.Parameters.AddWithValue("@ApplicationId", applicationId);

                    // Add a parameter to capture the return value
                    var returnValue = new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Retrieve the return value from the stored procedure
                    result = (int)returnValue.Value;
                }
            }

            // Return true if the result is 1 (all tests passed), otherwise false
            return result == 1;
        }

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, Test entity)
        {
            throw new NotImplementedException();
        }
    }
}
