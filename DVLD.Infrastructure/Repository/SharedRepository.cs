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


    }
}   
