using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
using DVLD.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly IMapper _mapper;

        public PersonRepository(IMapper mapper) : base(mapper)
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


    }

}
