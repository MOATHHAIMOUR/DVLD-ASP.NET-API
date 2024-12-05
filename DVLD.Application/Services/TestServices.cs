using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.TestDTOs;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.Test;

namespace DVLD.Application.Services
{
    public class TestServices : ITestServices
    {
        private readonly IDBViewsRepository _dBViewsRepository;
        private readonly ITestAppointmentRepository _testAppointmentRepository;
        private readonly ITestRepository _testRepository;

        public TestServices(IDBViewsRepository dBViewsRepository, ITestAppointmentRepository testAppointmentRepository, ITestRepository testRepository)
        {
            _dBViewsRepository = dBViewsRepository;
            _testAppointmentRepository = testAppointmentRepository;
            _testRepository = testRepository;
        }

        public async Task<Result<ScheduleTestView>> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var result = await _dBViewsRepository.GetScheduleTestInfoAsync(localDrivingLicenseApplicationId, testTypeId);


            if (result.LocalDrivingLicenseApplicationId == 0)
                return Result<ScheduleTestView>.Failure(Error.RecoredNotFound("local application is not found"));
            else
                return Result<ScheduleTestView>.Success(result);


        }

        public async Task<Result<string>> AddTestAppointmentAsync(TestAppointment testAppointment)
        {
            // Check if the applicant has an unlocked test appointment
            bool hasUnlockedAppointment = await _testAppointmentRepository.IsApplicantHasAppointmentTestNotLockedAsync(
                testAppointment.LocalDrivingLicenseApplicationId,
                testAppointment.TestTypeId
            );

            if (hasUnlockedAppointment)
            {
                return Result<string>.Failure(Error.ValidationError("The applicant already has an active test appointment."));
            }

            // Check if the applicant has already passed the test
            bool hasPassedTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                testAppointment.LocalDrivingLicenseApplicationId,
                testAppointment.TestTypeId
            );

            if (hasPassedTest)
            {
                return Result<string>.Failure(Error.ValidationError("The applicant has already passed this test."));
            }


            // Add the test appointment and get the inserted ID
            int insertedId = await _testAppointmentRepository.AddAsync(
                TestStoredProcedures.SP_AddTestAppointment,
                testAppointment,
                new AddTestAppintmentDTO()
            );

            return Result<string>.Success($"Test appointment created successfully with ID: {insertedId}");
        }


        public async Task<Result<IEnumerable<TestAppointmentView>>> GetTestAppointmentViewAsync(SearchTestAppointmentViewDTO searchTestAppointmentViewDTO)
        {
            var result = await _dBViewsRepository.GetTestAppointmentViewAsync(searchTestAppointmentViewDTO);

            return Result<IEnumerable<TestAppointmentView>>.Success(result);
        }

        public async Task<Result<TestLocalApplicationView>> GetTestLocalApplicationDetial(int LocalApplicationId)
        {
            var TestlocalApplicationDetial = await _dBViewsRepository.TestLocalApplicationViewAsync(LocalApplicationId);

            if (TestlocalApplicationDetial.LocalDrivingLicenseApplicationId == 0)
                return Result<TestLocalApplicationView>.Failure(Error.RecoredNotFound("local application is not found"));
            else
                return Result<TestLocalApplicationView>.Success(TestlocalApplicationDetial);


        }

        public async Task<Result<string>> AddTestResult(Test test)
        {
            var result = await _testRepository.AddAsync(TestStoredProcedures.SP_AddTestResult, test, new AddTestDTO());

            return Result<string>.Success("Test result added successfully");
        }
    }
}
