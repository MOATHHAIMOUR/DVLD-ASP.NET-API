using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Domain.Entites;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.LocalDrivingApplication;

namespace DVLD.Application.Services.IServices
{
    public interface ILocalDrivingLicenseApplicationServices
    {
        public Task<Result<IEnumerable<LocalDrivingApplicationView>>> GetLocalDrivingApplicationView(SearchLocalDrivingApplicationViewDto searchLocalDrivingApplicationViewDto);

        public Task<Result<string>> AddNewLocalDrivingLicense(LocalDrivingLicenseApplication LocalDrivingLicenseApplication);

        public Task<Result<IEnumerable<LicenseClass>>> GetLicenseClassesAsync();

        public Task<Result<LicenseDetailsView>> GetLicenseViewAsync(int? ApplicationId, int? LicenseId);

        public Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId);

        public Task<Result<RenewLocalLicenseResultDTO>> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate);


    }
}
