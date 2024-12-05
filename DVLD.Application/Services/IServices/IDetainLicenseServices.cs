using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;

namespace DVLD.Application.Services.IServices
{
    public interface IDetainLicenseServices
    {
        public Task<Result<int>> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense);
    }
}
