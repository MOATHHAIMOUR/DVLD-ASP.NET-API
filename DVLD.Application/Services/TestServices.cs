using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
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
        private readonly ILocalDrivingApplicationRepository _localDrivingApplicationRepository;

        public TestServices(IDBViewsRepository dBViewsRepository, ITestAppointmentRepository testAppointmentRepository, ITestRepository testRepository, ILocalDrivingApplicationRepository localDrivingApplicationRepository)
        {
            _dBViewsRepository = dBViewsRepository;
            _testAppointmentRepository = testAppointmentRepository;
            _testRepository = testRepository;
            _localDrivingApplicationRepository = localDrivingApplicationRepository;
        }

        public async Task<Result<ScheduleAndTake_TestView>> GetScheduleTestInfoAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var result = await _dBViewsRepository.GetScheduleTestInfoAsync(localDrivingLicenseApplicationId, testTypeId);


            if (result == null)
                return Result<ScheduleAndTake_TestView>.Failure(Error.RecoredNotFound("local application is not found"));
            else
                return Result<ScheduleAndTake_TestView>.Success(result);


        }

        public async Task<Result<string>> AddTestAppointmentAsync(TestAppointment testAppointment)
        {
            // cheek if application is completed before proceed to other logic 

            bool result = await _localDrivingApplicationRepository.IsLocalDrivingApplicationCompeletedOrCancelled(testAppointment.LocalDrivingLicenseApplicationId);

            if (result)
                return Result<string>.Failure(Error.ValidationError("The application locked"));

            // Check if the applicant has an unlocked test appointment
            bool hasUnlockedAppointment = await _testAppointmentRepository.IsApplicantHasAppointmentTestNotLockedAsync(
                testAppointment.LocalDrivingLicenseApplicationId,
                (int)testAppointment.TestTypeId
            );

            if (hasUnlockedAppointment)
            {
                return Result<string>.Failure(Error.ValidationError("The applicant already has an active test appointment."));
            }

            if (testAppointment.TestTypeId == EnumTestType.VisionTest)
            {
                // Check if the applicant has already passed the test
                bool hasPassVisionTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                    testAppointment.LocalDrivingLicenseApplicationId,
                    (int)EnumTestType.VisionTest
                );

                if (hasPassVisionTest)
                {
                    return Result<string>.Failure(Error.ValidationError("The applicant has already passed this test."));
                }
            }
            else if (testAppointment.TestTypeId == EnumTestType.WrittenTheoryTest)
            {
                bool hasPassVisionTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                   testAppointment.LocalDrivingLicenseApplicationId,
                   (int)EnumTestType.VisionTest
                );

                if (!hasPassVisionTest)
                {
                    return Result<string>.Failure(Error.ValidationError("The applicant must pass vistion test to take written test."));
                }

                bool hasPassWrittenTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                   testAppointment.LocalDrivingLicenseApplicationId,
                   (int)EnumTestType.WrittenTheoryTest
                );

                if (hasPassWrittenTest)
                {
                    return Result<string>.Failure(Error.ValidationError("The applicant has already pass this test."));
                }
            }
            else
            {
                bool hasPassVisionTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                  testAppointment.LocalDrivingLicenseApplicationId,
                  (int)EnumTestType.VisionTest
                );

                if (!hasPassVisionTest)
                {
                    return Result<string>.Failure(Error.ValidationError("The applicant must pass vistion test to take written test."));
                }

                bool hasPassWrittenTest = await _testAppointmentRepository.IsApplicantPassTestAsync(
                   testAppointment.LocalDrivingLicenseApplicationId,
                   (int)EnumTestType.WrittenTheoryTest
                );

                if (!hasPassWrittenTest)
                {
                    return Result<string>.Failure(Error.ValidationError("The applicant must pass written test to take pratical test."));
                }

            }



            // Add the test appointment and get the inserted ID
            int insertedId = await _testAppointmentRepository.AddAsync(
                TestStoredProcedures.SP_AddTestAppointment,
                testAppointment
            );





            return Result<string>.Success($"Test appointment created successfully with ID: {insertedId}");
        }


        public async Task<Result<string>> AddTestResult(Test test)
        {
            var result = await _testRepository.AddAsync(TestStoredProcedures.SP_AddTestResult, test);



            return Result<string>.Success("Test result added successfully");
        }

        public async Task<Result<TestAppointmentDetailInfo>> GetTestAppointmentView(int LocalDrivingApplicationId)
        {
            var result = await _dBViewsRepository.GetTestAppointmentDetailInfo(LocalDrivingApplicationId);

            if (result == null)
                return Result<TestAppointmentDetailInfo>.Failure(Error.RecoredNotFound("Test AppointmentView is not found"));

            return Result<TestAppointmentDetailInfo>.Success(result);
        }

        public async Task<Result<IEnumerable<TestAppointmentView>>> GetTestAppointmentsPerTestType(int LocalApplicationID, int TestTypeId)
        {
            var result = await _dBViewsRepository.GetTestAppointmentsPerTestType(LocalApplicationID, TestTypeId);

            return Result<IEnumerable<TestAppointmentView>>.Success(result);
        }


    }
}
