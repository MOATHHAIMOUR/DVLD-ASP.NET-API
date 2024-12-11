using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class SharedRepository : ISharedRepository
    {

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            List<Country> countries = [];

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var cmd = new SqlCommand("SP_GetAllCountries", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            Country country = new()
                            {
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryID")),
                                Name = reader.GetString(reader.GetOrdinal("CountryName")),
                            };

                            countries.Add(country);
                        }
                    }
                }
            }

            return countries;
        }

        public async Task<List<ApplicationType>> GetAllApplicationTypesAsync()
        {
            List<ApplicationType> applicationTypes = new List<ApplicationType>();

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                // Use the stored procedure
                SqlCommand command = new SqlCommand("SP_GetAllApplicationTypes", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync(); // Open the connection asynchronously
                using (SqlDataReader reader = await command.ExecuteReaderAsync()) // Execute the stored procedure
                {
                    while (await reader.ReadAsync()) // Read rows asynchronously
                    {
                        applicationTypes.Add(new ApplicationType
                        {
                            ApplicationTypeId = Convert.ToInt32(reader["ApplicationTypeID"]),
                            ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString()!,
                            ApplicationFees = Convert.ToDecimal(reader["ApplicationFees"])
                        });
                    }
                }
            }

            return applicationTypes;
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            Country country = null!;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP_GetPersonCountry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for the stored procedure
                    command.Parameters.AddWithValue("@CountryId", countryId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            country = new Country
                            {
                                CountryId = Convert.ToInt32(reader["CountryID"]),
                                Name = Convert.ToString(reader["CountryName"]!)!,
                                // Add other fields from the Countries table as needed
                            };
                        }
                    }
                }
            }

            return country;
        }

        public async Task<bool> UpdateApplicationType(ApplicationType applicationType)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                // Define the command and set the stored procedure
                SqlCommand command = new SqlCommand("SP_UpdateApplicationType", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add parameters for the stored procedure
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationType.ApplicationTypeId);
                command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationType.ApplicationTypeTitle);
                command.Parameters.AddWithValue("@ApplicationFees", applicationType.ApplicationFees);

                // Open the connection asynchronously
                await connection.OpenAsync();

                // Execute the command asynchronously
                int rowsAffected = await command.ExecuteNonQueryAsync();

                // Return true if one or more rows were affected
                return rowsAffected > 0;

            }
        }

        public async Task<int> GetRowCountAsync(string tableName)
        {

            await using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await using (SqlCommand command = new SqlCommand("SP_GenericRowCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the table name as a parameter
                    command.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 128)
                    {
                        Value = tableName
                    });

                    await connection.OpenAsync();

                    // Execute the stored procedure and return the row count
                    object result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int rowCount))
                    {
                        return rowCount;
                    }
                    else
                    {
                        throw new Exception("Failed to retrieve row count.");
                    }
                }
            }
        }
    }
}
