using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;
using DVLD.Domain.views;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Numerics;

namespace DVLD.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
   
        

        public Task AddAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int personId)
        {
            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                int rowsAffected = 0;
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DeletePersonById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter for PersonId
                    command.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = personId });

                    // Execute the command
                    rowsAffected = await command.ExecuteNonQueryAsync();

                }
                return rowsAffected > 0;
            }
        }
     
        public Task<Person> GetByIdAsync(int id)
        {
                throw new NotImplementedException();
        }

        public Task UpdateAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PeopleView>> GetPeopleViewAsync(int? PersonId = null, string? NationalNo = null, string? FirstName = null, string? SecondName = null, string? ThirdName = null, string? LastName = null, EnumGender? Gender = null, string? Phone = null, string? Email = null, string? CountryName = null)
        {
            List<PeopleView> persons = [];

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var cmd = new SqlCommand("SP_GetPeople", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonId", PersonId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NationalNo", NationalNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SecondName", SecondName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ThirdName", ThirdName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastName", LastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (Gender==null) ? DBNull.Value : (Gender == EnumGender.Male ? "M" : "F"));
                    cmd.Parameters.AddWithValue("@Phone", Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CountryName", CountryName ?? (object)DBNull.Value);

                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            PeopleView person = new()
                            {
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                SecondName = reader.IsDBNull(reader.GetOrdinal("SecondName")) ? null : (string)reader["SecondName"],
                                ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : (string)reader["ThirdName"],
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Gender = (reader.GetString(reader.GetOrdinal("Gender"))[0])=='M'?EnumGender.Male:EnumGender.Female,
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Image = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : (byte[])reader["ImagePath"], // Read binary data
                                CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                            };

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }
    }
}   
