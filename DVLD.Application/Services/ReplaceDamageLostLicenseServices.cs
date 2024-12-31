using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using DVLD.Application.Services.IServices;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Application.Services
{
    public class ReplaceDamageLostLicenseServices : IReplaceDamageLostLicenseServices
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IDBViewsRepository _dBViewsRepository;

        public ReplaceDamageLostLicenseServices(ILicenseRepository licenseRepository, IDBViewsRepository dBViewsRepository)
        {
            _licenseRepository = licenseRepository;
            _dBViewsRepository = dBViewsRepository;
        }

        public async Task<Result<ReplaceDamageResultDTO>> ReplaceDamageLicenseAsync(ReplaceDamageLostDTO replaceDamageDTO)
        {
            // Retrieve license information
            var license = await _dBViewsRepository.GetLicenseInfo(null, replaceDamageDTO.LicenseId, null);
            if (license == null)
                return Result<ReplaceDamageResultDTO>.Failure(Error.RecoredNotFound("License is not found"));

            // Check if the license is active
            if (!license.IsActive)
                return Result<ReplaceDamageResultDTO>.Failure(Error.ValidationError("License is not active"));

            // Replace the damaged license
            (int applicationId, int replacementLostLocalDrivingLicense) =
                await _licenseRepository.ReplaceLostLicenseAsync(
                    "SP_ReplaceForDamageLocalDrivingLicense",
                    replaceDamageDTO.LicenseId,
                    replaceDamageDTO.CreatedByUser
                );

            // Prepare the result DTO
            var resultDTO = new ReplaceDamageResultDTO
            {
                ApplicationId = applicationId,
                ReplacementDamageForLicenseId = replacementLostLocalDrivingLicense
            };

            return Result<ReplaceDamageResultDTO>.Success(resultDTO);
        }

        public async Task<Result<ReplaceLostResultDTO>> ReplaceLostLicenseAsync(ReplaceDamageLostDTO replaceDamageDTO)
        {
            // Retrieve license information
            var license = await _dBViewsRepository.GetLicenseInfo(null, replaceDamageDTO.LicenseId, null);
            if (license == null)
                return Result<ReplaceLostResultDTO>.Failure(Error.RecoredNotFound("License is not found"));

            // Check if the license is active
            if (!license.IsActive)
                return Result<ReplaceLostResultDTO>.Failure(Error.ValidationError("License is not active"));

            // Replace the lost license
            (int applicationId, int replacementLostLocalDrivingLicense) =
                await _licenseRepository.ReplaceLostLicenseAsync(
                    "SP_ReplaceForLostLocalDrivingLicense",
                    replaceDamageDTO.LicenseId,
                    replaceDamageDTO.CreatedByUser
                );

            // Prepare the result DTO
            var resultDTO = new ReplaceLostResultDTO
            {
                ApplicationId = applicationId,
                ReplacementForLostLicenseId = replacementLostLocalDrivingLicense
            };

            return Result<ReplaceLostResultDTO>.Success(resultDTO);
        }

    }
}
