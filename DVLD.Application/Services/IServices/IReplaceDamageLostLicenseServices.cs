using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;

namespace DVLD.Application.Services.IServices
{
    public interface IReplaceDamageLostLicenseServices
    {
        public Task<Result<ReplaceDamageResultDTO>> ReplaceDamageLicenseAsync(ReplaceDamageLostDTO replaceDamageDTO);

        public Task<Result<ReplaceLostResultDTO>> ReplaceLostLicenseAsync(ReplaceDamageLostDTO replaceDamageDTO);

    }
}
