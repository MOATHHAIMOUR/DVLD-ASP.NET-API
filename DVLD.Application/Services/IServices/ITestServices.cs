using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;
using DVLD.Domain.views.Test;

namespace DVLD.Application.Services.IServices
{
    public interface ITestServices
    {

        public Task<Result<TestAppointmentDetailInfo>> GetTestAppointmentView(int LocalDrivingApplicationId);

        public Task<Result<string>> AddTestAppointmentAsync(TestAppointment testAppointmentDTO);

        public Task<Result<ScheduleAndTake_TestView>> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId);

        public Task<Result<string>> AddTestResult(Test test);



        public Task<Result<IEnumerable<TestAppointmentView>>> GetTestAppointmentsPerTestType(int LocalApplicationID, int TestTypeId);

    }
}
