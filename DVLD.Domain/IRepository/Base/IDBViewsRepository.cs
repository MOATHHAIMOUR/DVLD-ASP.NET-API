using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.LocalDrivingApplication;
using DVLD.Domain.views.Person;
using DVLD.Domain.views.Test;

namespace DVLD.Domain.IRepository.Base
{
    public interface IDBViewsRepository
    {
        public Task<IEnumerable<PersonView?>> GetPeopleViewAync(string storedProcedure, PeopleSearchParameters PeopleSearchParameters);
        public Task<TestAppointmentDetailInfo?> GetTestAppointmentDetailInfo(int LocalApplicationID);
        public Task<LicenseDetailsView?> GetLicenseInfo(int? ApplicationId, int? LicenseId, int? localDrivingApplicationId);
        public Task<ScheduleAndTake_TestView?> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId);
        public Task<InternationalLicenseView?> GetInternationalLicenseViewAsync(int InternationalLicenseId);
        public Task<IEnumerable<LocalDrivingApplicationView>> GetLocalDrivingApplicationsView(string storedProcedure, LocalDrivingApplicationsSearchParameters localDrivingApplicationsSearchParameters);

        public Task<IEnumerable<TestAppointmentView>> GetTestAppointmentsPerTestType(int LocalApplicationID, int TestTypeId);

    }
}
