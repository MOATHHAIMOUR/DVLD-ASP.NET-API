using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private IMapper _mapper;

        public PersonRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Person?> GetPersonByIdOrNationalNo(int? personId, string? nationalNo)
        {
            Person? person = null;

            using (SqlConnection connection = new(DbSettings._connectionString))
            {
                using SqlCommand command = new(PersonStoredProcedures.SP_GetPersonByPersonIdOrNationalNo, connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personId.HasValue ? (object)personId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@NationalNO", nationalNo ?? (object)DBNull.Value);

                await connection.OpenAsync();

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    person = _mapper.Map<Person>(reader);
                }
            }

            return person;
        }

        public async Task<bool> UpdateAsync(string storedProcedure, Person entity)
        {
            // Ensure you have a valid database connection string
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to match the properties of the Person entity
                    command.Parameters.AddWithValue("@PersonId", entity.PersonId);
                    command.Parameters.AddWithValue("@NationalNo", entity.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                    command.Parameters.AddWithValue("@SecondName", entity.SecondName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ThirdName", entity.ThirdName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", entity.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", (int)entity.Gender);
                    command.Parameters.AddWithValue("@Phone", entity.Phone);
                    command.Parameters.AddWithValue("@Address", entity.Address);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@CountryId", entity.CountryId);
                    command.Parameters.AddWithValue("@ImagePath", entity.ImagePath);

                    // Execute the command
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was affected, otherwise false
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<int> AddAsync(string storedProcedure, Person entity)
        {
            // Ensure you have a valid database connection string
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to match the properties of the Person entity
                    command.Parameters.AddWithValue("@NationalNo", entity.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                    command.Parameters.AddWithValue("@SecondName", entity.SecondName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ThirdName", entity.ThirdName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", entity.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", (int)entity.Gender);
                    command.Parameters.AddWithValue("@Phone", entity.Phone);
                    command.Parameters.AddWithValue("@Address", entity.Address);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@CountryId", entity.CountryId);
                    command.Parameters.AddWithValue("@ImagePath", entity.ImagePath ?? (object)DBNull.Value);


                    // Execute the command and retrieve the newly created ID
                    var newPersonId = 0;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            newPersonId = Convert.ToInt32(reader["NewPersonID"]);
                        }
                    }

                    return newPersonId;
                }
            }
        }

        public async Task<Person?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            Person? person = null;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            // Map the SQL row to a Person object
                            person = _mapper.Map<IDataReader, Person>(reader);
                        }
                    }
                }
            }

            return person;
        }

        public async Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    // Execute the command and check the number of affected rows
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was affected, otherwise false
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the property name and value as a parameter
                    command.Parameters.AddWithValue($"@{propertyName}", value);

                    // Execute the command and retrieve the result
                    var result = await command.ExecuteScalarAsync();

                    // Check if the result is not null and greater than zero
                    return result != null && Convert.ToInt32(result) > 0;
                }
            }
        }

        public async Task<IEnumerable<Person>> GetAllAsync(string storedProcedure)
        {
            var persons = new List<Person>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var person = _mapper.Map<IDataReader, Person>(reader);
                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }

    }

}
