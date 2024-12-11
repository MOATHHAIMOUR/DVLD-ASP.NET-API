using AutoMapper;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.Person;
using DVLD.Domain.views.Test;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.Infrastructure.Repository.Views
{
    public class DBViewsRepository : IDBViewsRepository
    {
        private readonly IMapper _mapper;

        public DBViewsRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonView>> GetPeopleViewAync(string storedProcedure, PeopleSearchParameters PeopleSearchParameters)
        {
            var people = new List<PersonView>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters dynamically based on PeopleSearchParameters
                    command.Parameters.AddWithValue("@PersonId", PeopleSearchParameters.PersonId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NationalNo", PeopleSearchParameters.NationalNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FirstName", PeopleSearchParameters.FirstName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SecondName", PeopleSearchParameters.SecondName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ThirdName", PeopleSearchParameters.ThirdName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", PeopleSearchParameters.LastName ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@Gender", PeopleSearchParameters.Gender == null ? (object)DBNull.Value : PeopleSearchParameters.Gender == EnumGender.Male ? 'M' : 'F');

                    command.Parameters.AddWithValue("@Phone", PeopleSearchParameters.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", PeopleSearchParameters.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CountryName", PeopleSearchParameters.CountryName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PageNumber", PeopleSearchParameters.PageNumber ?? 1);
                    command.Parameters.AddWithValue("@PageSize", PeopleSearchParameters.PageSize ?? 10);
                    command.Parameters.AddWithValue("@OrderBy", PeopleSearchParameters.OrderBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@OrderDirection", PeopleSearchParameters.OrderDirection ?? "ASC");


                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            people.Add(_mapper.Map<PersonView>(reader));
                        }
                    }
                }
            }

            return people;
        }

        public async Task<TestLocalApplicationView> TestLocalApplicationViewAsync(int LocalApplicationID)
        {
            var result = new TestLocalApplicationView();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(LocalDrivingApplicationStoredProcedures.SP_GetLocalTestApplicationDetails, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for the stored procedure
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", LocalApplicationID);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = _mapper.Map<TestLocalApplicationView>(reader);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<TestAppointmentView>> GetTestAppointmentViewAsync(object SearchParameters)
        {
            List<TestAppointmentView> appointments = [];

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetTestAppointmentView", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    await connection.OpenAsync();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            var appointment = _mapper.Map<TestAppointmentView>(reader);
                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public async Task<ScheduleTestView> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var result = new ScheduleTestView();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(ViewsStoredProcedures.SP_GetScheduleTestData, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
                    command.Parameters.AddWithValue("@TestTypeId", testTypeId);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Use AutoMapper to map IDataRecord to ScheduleTestView
                            result = _mapper.Map<ScheduleTestView>(reader);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<LicenseDetailsView?> GetLicenseByApplicationIdOrLicenseIdAsync(int? ApplicationId, int? LicenseId)
        {
            LicenseDetailsView? result = null;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                string stored = "";
                string parameter = "";
                int Id = 0;
                if (ApplicationId.HasValue)
                {
                    stored = "SP_GetLicenseByApplicationId";
                    parameter = "@ApplicationId";
                    Id = ApplicationId.Value;
                }
                else
                {
                    stored = "SP_GetLicenseByLicenseId";
                    parameter = "@LicenseId";
                    Id = LicenseId!.Value;
                }

                using (var command = new SqlCommand(stored, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;



                    command.Parameters.AddWithValue(parameter, Id);

                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = _mapper.Map<LicenseDetailsView>(reader);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<InternationalLicenseView?> GetInternationalLicenseViewAsync(int InternationalLicenseId)
        {
            using (var connection = new SqlConnection(DbSettings._connectionString))

            using (var command = new SqlCommand("SP_GetInternationalLicenseDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@InternationalLicenseId", SqlDbType.Int)
                {
                    Value = InternationalLicenseId
                });

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return _mapper.Map<InternationalLicenseView>(reader);
                    }
                }
            }

            return null;
        }

    }
}
