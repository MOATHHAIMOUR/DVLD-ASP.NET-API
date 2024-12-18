using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly IMapper _mapper;

        public LicenseRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(string storedProcedure, License entity)
        {
            int newLicenseId = 0;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                command.Parameters.AddWithValue("@ApplicationId", entity.ApplicationId);
                command.Parameters.AddWithValue("@LicenseClassId", entity.LicenseClassId);
                command.Parameters.AddWithValue("@Notes", entity.Notes ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedByUserId", entity.CreatedByUserId);

                // Add output parameter to capture the new license ID
                var outputParam = new SqlParameter
                {
                    ParameterName = "NewLicenseId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                // Open the connection
                await connection.OpenAsync();

                // Execute the command
                await command.ExecuteNonQueryAsync();

                // Get the output parameter value
                newLicenseId = Convert.ToInt32(outputParam.Value);
            }

            return newLicenseId;
        }

        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<License>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<License?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, License entity)
        {
            throw new NotImplementedException();
        }
    }
}
