using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.DetainLicenseDtos;
using DVLD.Domain.Entites;

namespace DVLD.Application.Services.IServices
{
    public interface IDetainLicenseServices
    {
        public Task<Result<int>> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense);

        public Task<Result<int>> ReleaseDetainLocalDrivingLicenseAsync(LicenseReleaseDTO licenseReleaseDTO);
    }
}
