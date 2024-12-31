using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Services.IServices;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.LocalDrivingApplication;

namespace DVLD.Application.Services
{
    public class LocalDrivingApplicationServices : ILocalDrivingLicenseApplicationServices
    {
        private readonly ILocalDrivingApplicationRepository _localDrivingApplicationRepository;
        private readonly IDBViewsRepository _dBViewsRepository;
        private readonly ITestRepository _testRepository;
        private readonly ILicenseRepository _licenseRepository;

        public LocalDrivingApplicationServices(ILocalDrivingApplicationRepository localDrivingApplicationRepository, IDBViewsRepository dBViewsRepository, ITestRepository testRepository, ILicenseRepository licenseRepository)
        {
            _localDrivingApplicationRepository = localDrivingApplicationRepository;
            _dBViewsRepository = dBViewsRepository;
            _testRepository = testRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task<Result<string>> AddNewLocalDrivingLicenseApplication(LocalDrivingLicenseApplication localDrivingLicenseApplication)
        {
            // Check if the person already has a license of the same type
            bool hasActiveLicense = await _localDrivingApplicationRepository.IsApplicanHasAlreadyActiveLicenseWithSameType(
                localDrivingLicenseApplication.Application.ApplicantPersonId,
                (int)localDrivingLicenseApplication.LicenseClassId
            );

            if (hasActiveLicense)
            {
                return Result<string>.Failure(Error.ValidationError("This person already has a license of the same type."));
            }

            // Check if the person already has an active local driving license application
            bool hasActiveApplication = await _localDrivingApplicationRepository.IsApplicantHasAcActiveLocalDrivingApplication(
                localDrivingLicenseApplication.Application.ApplicantPersonId
                ,
                (int)localDrivingLicenseApplication.LicenseClassId
            );

            if (hasActiveApplication)
            {
                return Result<string>.Failure(Error.ValidationError("This person already has an active application with the same license type."));
            }



            // Add the new local driving license application
            int insertedID = await _localDrivingApplicationRepository.AddAsync(
                LocalDrivingApplicationStoredProcedures.SP_AddNewLocalDrivingLicenseApplication,
                localDrivingLicenseApplication
            );

            if (insertedID <= 0)
            {
                return Result<string>.Failure(Error.ValidationError("Failed to add the application. Please try again."));
            }

            return Result<string>.Success($"Application is added successfully with ID: {insertedID}");
        }

        public async Task<Result<string>> CancelLocalDrivingApplication(int localDrivingApplicationId)
        {

            bool isApplicationCompleted = await _localDrivingApplicationRepository.IsLocalDrivingApplicationCompletedAsync(localDrivingApplicationId);
            if (isApplicationCompleted)
                return Result<string>.Failure(Error.ValidationError("The application is completed can't cancel it"));

            var result = await _localDrivingApplicationRepository.CancelLocalDrivingApplication(localDrivingApplicationId);

            return Result<string>.Success("Application is Cancelled successfully");
        }

        public async Task<Result<IEnumerable<LicenseClass>>> GetLicenseClassesAsync()
        {
            //var License = await _localDrivingApplicationRepository.AddAsync(LocalDrivingApplicationStoredProcedures.SP_GetAllLicenseClasses);

            //return Result<IEnumerable<LicenseClass>>.Success(License);

            throw new NotImplementedException();
        }


        public async Task<Result<string>> AddLicensesForFirstTimeAsync(License license)
        {
            bool isApplicantPassAllTests = await _testRepository.IsApplicantPassAllTests(TestStoredProcedures.SP_IsApplicantPassAllTests, license.ApplicationId);


            if (!isApplicantPassAllTests)
                return Result<string>.Failure(Error.ValidationError("Applicant dose not pass all tests"));

            int insertedId = await _licenseRepository.AddAsync(LicensesStoredProcedure.SP_AddNewLocalLicense, license);

            return Result<string>.Success($"New License with Id ${insertedId} added successfully.");
        }

        public async Task<Result<LicenseDetailsView>> GetLicenseViewAsync(int? ApplicationId, int? LicenseId, int? localDrivingApplicationId)
        {
            var licenseView = await _dBViewsRepository.GetLicenseInfo(ApplicationId, LicenseId, localDrivingApplicationId);

            if (licenseView == null)
            {
                return Result<LicenseDetailsView>.Failure(Error.RecoredNotFound("License is not found"));
            }
            else
            {
                return Result<LicenseDetailsView>.Success(licenseView);
            }
        }




        public async Task<Result<IEnumerable<LocalDrivingApplicationView>>> GetLocalDrivingApplicationView(LocalDrivingApplicationsSearchParameters localDrivingApplicationsSearchParameters)
        {
            var result = await _dBViewsRepository.GetLocalDrivingApplicationsView(LocalDrivingApplicationStoredProcedures.SP_GetApplicationView, localDrivingApplicationsSearchParameters);

            return Result<IEnumerable<LocalDrivingApplicationView>>.Success(result);

        }

        public Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<RenewLocalLicenseResultDTO>> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId)
        {
            LicenseDetailsView? license = await _dBViewsRepository.GetLicenseInfo(null, licenseId, null);

            if (license == null)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.RecoredNotFound("License is not found"));


            if (license.IsDetain)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License detained can't renew it"));


            if (!license.IsActive)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License is not active can't renew it"));


            if (license.ExpirationDate > DateTime.UtcNow)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License is not Expired can't renew it"));


            (int ApplicationId, int RenewLicenseId) = await _localDrivingApplicationRepository.RenewLocalDrivingLicenseAsync(licenseId, createdByUserId);

            return Result<RenewLocalLicenseResultDTO>.Success(new RenewLocalLicenseResultDTO
            {
                ApplicationId = ApplicationId,
                RenewLicenseId = RenewLicenseId,

            });
        }



    }
}
