using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.CancelLocalDrivingApplicaiton
{
    public class CancelLocalDrivingApplicaitonCommandHandler : IRequestHandler<CancelLocalDrivingApplicaitonCommand, ApiResponse<string>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingLicenseApplicationServices;

        public CancelLocalDrivingApplicaitonCommandHandler(ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices)
        {
            _localDrivingLicenseApplicationServices = localDrivingLicenseApplicationServices;
        }

        public async Task<ApiResponse<string>> Handle(CancelLocalDrivingApplicaitonCommand request, CancellationToken cancellationToken)
        {
            var result = await _localDrivingLicenseApplicationServices.CancelLocalDrivingApplication(request.localDrivingApplicationId);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<string>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
