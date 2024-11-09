using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public Task AddAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Person>> GetAllAsync()
        {
            List<Person> persons = new List<Person>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllPersons", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                   
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Person person = new()
                            {
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                                NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                SecondName = reader.GetString(reader.GetOrdinal("SecondName")),
                                ThirdName = reader.GetString(reader.GetOrdinal("ThirdName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Gender = reader.GetChar(reader.GetOrdinal("Gender")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                                Image = (byte[])reader["Image"]
                            };
                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }

        public Task<Person> GetByIdAsync(int id)
        {
                throw new NotImplementedException();
        }

        public Task UpdateAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}   
