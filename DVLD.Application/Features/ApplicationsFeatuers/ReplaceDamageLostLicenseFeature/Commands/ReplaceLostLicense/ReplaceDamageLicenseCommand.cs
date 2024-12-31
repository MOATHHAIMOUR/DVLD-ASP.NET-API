using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceLostLicense
{
    public class ReplaceLostLicenseCommand : IRequest<ApiResponse<ReplaceLostResultDTO>>
    {
        public ReplaceDamageLostDTO ReplaceDamageLost { get; set; }
        public ReplaceLostLicenseCommand(ReplaceDamageLostDTO replaceDamageLost)
        {
            ReplaceDamageLost = replaceDamageLost;
        }

    }
}
