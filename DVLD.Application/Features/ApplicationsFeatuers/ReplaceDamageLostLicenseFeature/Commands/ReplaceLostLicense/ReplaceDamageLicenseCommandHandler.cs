using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceLostLicense
{
    public class ReplaceLostLicenseCommandHandler : IRequestHandler<ReplaceLostLicenseCommand, ApiResponse<ReplaceLostResultDTO>>
    {
        private readonly IReplaceDamageLostLicenseServices _replaceDamageLostLicenseServices;

        public ReplaceLostLicenseCommandHandler(IReplaceDamageLostLicenseServices replaceDamageLostLicenseServices)
        {
            _replaceDamageLostLicenseServices = replaceDamageLostLicenseServices;
        }

        public async Task<ApiResponse<ReplaceLostResultDTO>> Handle(ReplaceLostLicenseCommand request, CancellationToken cancellationToken)
        {
            var result = await _replaceDamageLostLicenseServices.ReplaceLostLicenseAsync(request.ReplaceDamageLost);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<ReplaceLostResultDTO>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}



