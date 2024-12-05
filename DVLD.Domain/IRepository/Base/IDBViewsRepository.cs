using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.Test;

namespace DVLD.Domain.IRepository.Base
{
    public interface IDBViewsRepository
    {
        public Task<TestLocalApplicationView> TestLocalApplicationViewAsync(int LocalApplicationID);
        public Task<IEnumerable<TestAppointmentView>> GetTestAppointmentViewAsync(object SearchParameters);
        public Task<LicenseDetailsView?> GetLicenseByApplicationIdOrLicenseIdAsync(int? ApplicationId, int? LicenseId);
        public Task<ScheduleTestView> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId);
        public Task<InternationalLicenseView?> GetInternationalLicenseViewAsync(int InternationalLicenseId);

    }
}
