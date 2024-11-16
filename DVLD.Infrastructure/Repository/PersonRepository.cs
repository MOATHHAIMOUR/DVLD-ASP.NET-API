using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;
using DVLD.Domain.views;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public async Task<int> AddPersonAsycn(Person person)
        {
            int insertedPersonId = -1; 

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@NationalNo", person.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@SecondName", person.SecondName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", person.Gender == EnumGender.Male ? 'M' : 'F');
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", person.CountryId);
                    command.Parameters.AddWithValue("@ImagePath", person.ImageBytes ?? (object)DBNull.Value);

                  


                    // Open connection and execute the command
                    await connection.OpenAsync();
                    
                    insertedPersonId = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }

            return insertedPersonId;
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP_DeletePersonById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter for PersonId
                    command.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = personId });

                    // Execute the command
                    rowsAffected = await command.ExecuteNonQueryAsync();

                }
            }

            return rowsAffected > 0;
        }

        public Task<Person> GetByIdAsync(int id)
        {
                throw new NotImplementedException();
        }

        public async Task<List<PeopleView>> GetPeopleViewAsync(int? PersonId = null, string? NationalNo = null, string? FirstName = null, string? SecondName = null, string? ThirdName = null, string? LastName = null, EnumGender? Gender = null, string? Phone = null, string? Email = null, string? CountryName = null, int PageNumber = 1, int pageSize = 5, string? SortBy = null, EnumSortDirection sortDirection = EnumSortDirection.ASC)
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
                    cmd.Parameters.AddWithValue("@Gender", (Gender == null) ? DBNull.Value : (Gender == EnumGender.Male ? "M" : "F"));
                    cmd.Parameters.AddWithValue("@Phone", Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CountryName", CountryName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                    cmd.Parameters.AddWithValue("@orderBy",SortBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@orderDirection", sortDirection == EnumSortDirection.ASC? "ASC":"DESC");

                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            PeopleView person = new()
                            {
                                PersonId =    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                NationalNo =  reader.GetString(reader.GetOrdinal("NationalNo")),
                                FirstName =   reader.GetString(reader.GetOrdinal("FirstName")),
                                SecondName =  reader.IsDBNull(reader.GetOrdinal("SecondName")) ? null : (string)reader["SecondName"],
                                ThirdName =   reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : (string)reader["ThirdName"],
                                LastName =    reader.GetString(reader.GetOrdinal("LastName")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Gender =      (reader.GetString(reader.GetOrdinal("Gender"))[0]) == 'M' ? EnumGender.Male : EnumGender.Female,
                                Address =     reader.GetString(reader.GetOrdinal("Address")),
                                Phone =       reader.GetString(reader.GetOrdinal("Phone")),
                                Email =       reader.GetString(reader.GetOrdinal("Email")),
                                CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                           
                            };

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons; throw new NotImplementedException();
        }

        public async Task<Person?> GetPersonByIdOrNationalNo(int? personId, string? nationalNo)
        {
            Person? person = null;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPersonByPersonIdOrNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", personId.HasValue ? (object)personId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NationalNO", nationalNo ?? (object)DBNull.Value);

                    await  connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            person = new Person
                            {

                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                SecondName = reader.IsDBNull(reader.GetOrdinal("SecondName")) ? null : (string)reader["SecondName"],
                                ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : (string)reader["ThirdName"],
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Gender = (reader.GetString(reader.GetOrdinal("Gender"))[0]) == 'M' ? EnumGender.Male : EnumGender.Female,
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                ImageBytes = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : (byte[])reader["ImagePath"], // Read binary data



                            };
                    }   }
                }
            }

           return person;
        }

        public async Task<bool> UpdatePersonAsycn(Person person)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Map Person object properties to stored procedure parameters
                    command.Parameters.AddWithValue("@PersonID", person.PersonId);
                    command.Parameters.AddWithValue("@NationalNo", person.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@SecondName", person.SecondName);
                    command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", person.CountryId);

                    // Handle ImagePath (can be null)
                    if (person.ImageBytes == null)
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.VarBinary).Value = person.ImageBytes;
                    }

                    // Open the connection and execute the command
                    await connection.OpenAsync();
                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }

           return  affectedRows > 0;
        }
   
    }
}   
