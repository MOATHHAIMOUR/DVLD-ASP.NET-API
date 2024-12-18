using AutoMapper;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.LocalDrivingApplication;
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

        public async Task<IEnumerable<LocalDrivingApplicationView>> GetLocalDrivingApplicationsView(string storedProcedure, LocalDrivingApplicationsSearchParameters localDrivingApplicationsSearchParameters)
        {
            var results = new List<LocalDrivingApplicationView>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object?)localDrivingApplicationsSearchParameters.LocalDrivingLicenseApplicationID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ClassName", (object?)localDrivingApplicationsSearchParameters.ClassName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NationalNo", (object?)localDrivingApplicationsSearchParameters.NationalNo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FullName", (object?)localDrivingApplicationsSearchParameters.FullName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ApplicationDate", (object?)localDrivingApplicationsSearchParameters.ApplicationDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PassedTests", (object?)localDrivingApplicationsSearchParameters.PassedTests ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ApplicationStatus", (object?)localDrivingApplicationsSearchParameters.ApplicationStatus ?? DBNull.Value);
                    command.Parameters.AddWithValue("@OrderBy", (object?)localDrivingApplicationsSearchParameters.OrderBy ?? "ID");
                    command.Parameters.AddWithValue("@OrderDirection", (object?)localDrivingApplicationsSearchParameters.OrderDirection ?? "ASC");
                    command.Parameters.AddWithValue("@PageSize", localDrivingApplicationsSearchParameters.PageSize);
                    command.Parameters.AddWithValue("@PageNumber", localDrivingApplicationsSearchParameters.PageNumber);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var applicationView = _mapper.Map<LocalDrivingApplicationView>(reader);
                            results.Add(applicationView);
                        }
                    }
                }
            }

            return results;
        }

        public async Task<TestAppointmentDetailInfo?> GetTestAppointmentDetailInfo(int LocalApplicationID)
        {
            TestAppointmentDetailInfo? details = null;

            using (SqlConnection connection = new SqlConnection(DbSettings._connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetTestDrivingLicenseApplicationDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for the stored procedure
                    command.Parameters.Add(new SqlParameter("@LocalDrivingApplicationId", SqlDbType.Int)
                    {
                        Value = LocalApplicationID
                    });

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            details = _mapper.Map<TestAppointmentDetailInfo>(reader);
                        }
                    }
                }
            }

            return details;
        }

        public async Task<ScheduleAndTake_TestView?> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            ScheduleAndTake_TestView? ScheduleTestView = null;

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand(ViewsStoredProcedures.SP_GetTake_ScheduleTestData, connection))
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
                            ScheduleTestView = _mapper.Map<ScheduleAndTake_TestView>(reader);
                        }
                    }
                }
            }

            return ScheduleTestView;
        }

        public async Task<LicenseDetailsView?> GetLicenseInfo(int? applicationId, int? licenseId, int? localDrivingApplicationId)
        {
            LicenseDetailsView? result = null;


            using (var connection = new SqlConnection(DbSettings._connectionString)) // Ensure the connection string is properly initialized
            {
                using (var command = new SqlCommand("SP_GetLocalLicenseView", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with proper null handling
                    command.Parameters.AddWithValue("@ApplicationId", applicationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LicenseId", licenseId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LocalDrivingApplicationId", localDrivingApplicationId ?? (object)DBNull.Value);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Assuming you have a mapper to convert the reader to the desired view
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

        public async Task<IEnumerable<TestAppointmentView>> GetTestAppointmentsPerTestType(int LocalApplicationID, int TestTypeId)
        {
            var results = new List<TestAppointmentView>();

            using (var connection = new SqlConnection(DbSettings._connectionString))
            {
                using (var command = new SqlCommand("SP_GetTestAppoinments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LocalDrivingApplicationId", LocalApplicationID);
                    command.Parameters.AddWithValue("@TestTypeId", TestTypeId);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(_mapper.Map<TestAppointmentView>(reader));
                        }
                    }
                }
            }

            return results;
        }
    }
}
