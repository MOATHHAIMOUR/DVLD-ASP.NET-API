using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceDamageLicense
{
    public class ReplaceDamageLicenseCommand : IRequest<ApiResponse<ReplaceDamageResultDTO>>
    {
        public ReplaceDamageLostDTO ReplaceDamageLost { get; set; }

        public ReplaceDamageLicenseCommand(ReplaceDamageLostDTO replaceDamageLost)
        {
            ReplaceDamageLost = replaceDamageLost;
        }

    }
}
