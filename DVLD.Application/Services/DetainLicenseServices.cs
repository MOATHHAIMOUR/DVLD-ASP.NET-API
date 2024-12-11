using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.DetainLicenseDtos;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;

namespace DVLD.Application.Services
{
    public class DetainLicenseServices : IDetainLicenseServices
    {
        private readonly IDetainedLicenseRepository _detainedLicenseRepository;
        private readonly ILicenseRepository _licenseRepository;

        public DetainLicenseServices(IDetainedLicenseRepository detainedLicenseRepository, ILicenseRepository licenseRepository)
        {
            _detainedLicenseRepository = detainedLicenseRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task<Result<int>> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense)
        {
            License? license = await _licenseRepository.GetAsync(LicenseStoredProcedure.SP_GetLicenseById, "LicenseId", detainedLicense.LicenseId);

            if (license == null)
                return Result<int>.Failure(Error.RecoredNotFound("License is not found"));

            if (!license.IsActive)
                return Result<int>.Failure(Error.RecoredNotFound("License is not active can't detain it"));

            int insertedDetainId = await _detainedLicenseRepository.AddAsync(DetainLicenseStoredProcedures.SP_DetainLocalDrivingLicense, detainedLicense);

            return Result<int>.Success(insertedDetainId);
        }

        public async Task<Result<int>> ReleaseDetainLocalDrivingLicenseAsync(LicenseReleaseDTO licenseReleaseDTO)
        {
            License? license = await _licenseRepository.GetAsync(LicenseStoredProcedure.SP_GetLicenseById, "LicenseId", licenseReleaseDTO.LicenseId);

            if (license == null)
                return Result<int>.Failure(Error.RecoredNotFound("License is not found"));

            if (!license.IsActive)
                return Result<int>.Failure(Error.RecoredNotFound("License is not active can't detain it"));

            int releaseApplicationId = await _detainedLicenseRepository.ReleaseDetainLocalDrivingLicenseAsync(licenseReleaseDTO.LicenseId, licenseReleaseDTO.ReleasedDate, licenseReleaseDTO.ReleasedByUserId);

            return Result<int>.Success(releaseApplicationId);
        }
    }
}
