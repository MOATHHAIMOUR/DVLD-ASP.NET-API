using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceDamageLicense
{
    public class ReplaceDamageLicenseCommandHandler : IRequestHandler<ReplaceDamageLicenseCommand, ApiResponse<ReplaceDamageResultDTO>>
    {
        private readonly IReplaceDamageLostLicenseServices _replaceDamageLostLicenseServices;

        public ReplaceDamageLicenseCommandHandler(IReplaceDamageLostLicenseServices replaceDamageLostLicenseServices)
        {
            _replaceDamageLostLicenseServices = replaceDamageLostLicenseServices;
        }

        public async Task<ApiResponse<ReplaceDamageResultDTO>> Handle(ReplaceDamageLicenseCommand request, CancellationToken cancellationToken)
        {
            var result = await _replaceDamageLostLicenseServices.ReplaceDamageLicenseAsync(request.ReplaceDamageLost);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<ReplaceDamageResultDTO>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
