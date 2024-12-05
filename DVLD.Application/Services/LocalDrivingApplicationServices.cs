using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Services.IServices;
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

        public LocalDrivingApplicationServices(ILocalDrivingApplicationRepository localDrivingApplicationRepository, IDBViewsRepository dBViewsRepository)
        {
            _localDrivingApplicationRepository = localDrivingApplicationRepository;
            _dBViewsRepository = dBViewsRepository;
        }

        public async Task<Result<string>> AddNewLocalDrivingLicense(LocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        {
            int insertedID = await _localDrivingApplicationRepository.AddAsync(LocalDrivingApplicationStoredProcedures.SP_AddNewLocalDrivingLicenseApplication, LocalDrivingLicenseApplication, IncluedPropertyInSqlPrameter: new AddNewLocalDrivingLicenseDTO());

            if (insertedID <= 0)
            {
                return Result<string>.Failure(Error.ValidationError("This Person is already has an active application with the same license type."));
            }

            return Result<string>.Success($"Application is added successfully with id: {insertedID}");
        }

        public Task<Result<int>> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense)
        {
            _localDrivingApplicationRepository.DetainLocalDrivingLicenseAsync()
        }

        public async Task<Result<IEnumerable<LicenseClass>>> GetLicenseClassesAsync()
        {
            var License = await _localDrivingApplicationRepository.GetAllAsync<LicenseClass>(LocalDrivingApplicationStoredProcedures.SP_GetAllLicenseClasses);

            return Result<IEnumerable<LicenseClass>>.Success(License);
        }

        public async Task<Result<LicenseDetailsView>> GetLicenseViewAsync(int? ApplicationId, int? LicenseId)
        {
            var licenseView = await _dBViewsRepository.GetLicenseByApplicationIdOrLicenseIdAsync(ApplicationId, LicenseId);

            if (licenseView == null)
            {
                return Result<LicenseDetailsView>.Failure(Error.RecoredNotFound("License is not found"));
            }
            else
            {
                return Result<LicenseDetailsView>.Success(licenseView);
            }
        }

        public async Task<Result<IEnumerable<LocalDrivingApplicationView>>> GetLocalDrivingApplicationView(SearchLocalDrivingApplicationViewDto searchLocalDrivingApplicationViewDto)
        {
            var result = await _localDrivingApplicationRepository.GetAllAsync<LocalDrivingApplicationView>(LocalDrivingApplicationStoredProcedures.SP_GetApplicationView, searchLocalDrivingApplicationViewDto);

            return Result<IEnumerable<LocalDrivingApplicationView>>.Success(result);
        }

        public Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<RenewLocalLicenseResultDTO>> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate)
        {
            LicenseDetailsView? license = await _dBViewsRepository.GetLicenseByApplicationIdOrLicenseIdAsync(null, licenseId);

            if (license == null)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.RecoredNotFound("License is not found"));


            if (license.IsDetain)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License detained can't renew it"));


            if (!license.IsActive)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License is not active can't renew it"));


            if (license.ExpirationDate > DateTime.UtcNow)
                return Result<RenewLocalLicenseResultDTO>.Failure(Error.ValidationError("License is not Expired can't renew it"));


            (int ApplicationId, int RenewLicenseId) = await _localDrivingApplicationRepository.RenewLocalDrivingLicenseAsync(licenseId, createdByUserId, expirationDate);

            return Result<RenewLocalLicenseResultDTO>.Success(new RenewLocalLicenseResultDTO
            {
                ApplicationId = ApplicationId,
                RenewLicenseId = RenewLicenseId,

            });
        }



    }
}
