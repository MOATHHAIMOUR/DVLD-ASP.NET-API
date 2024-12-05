using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.TestDTOs;
using DVLD.Domain.Entites;
using DVLD.Domain.views.Test;

namespace DVLD.Application.Services.IServices
{
    public interface ITestServices
    {
        public Task<Result<TestLocalApplicationView>> GetTestLocalApplicationDetial(int LocalApplicationId);
        public Task<Result<IEnumerable<TestAppointmentView>>> GetTestAppointmentViewAsync(SearchTestAppointmentViewDTO searchTestAppointmentViewDTO);
        public Task<Result<string>> AddTestAppointmentAsync(TestAppointment testAppointmentDTO);
        public Task<Result<ScheduleTestView>> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId);

        public Task<Result<string>> AddTestResult(Test test);

    }
}
