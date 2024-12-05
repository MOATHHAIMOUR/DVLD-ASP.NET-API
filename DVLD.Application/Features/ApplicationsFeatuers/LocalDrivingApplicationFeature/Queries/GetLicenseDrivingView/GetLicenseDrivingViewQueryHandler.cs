using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.License.LocalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView
{
    public class GetLicenseDrivingViewQueryHandler : IRequestHandler<GetLicenseDrivingViewQuery, ApiResponse<LicenseDetailsView>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingLicenseApplicationServices;

        public GetLicenseDrivingViewQueryHandler(ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices)
        {
            _localDrivingLicenseApplicationServices = localDrivingLicenseApplicationServices;
        }

        public async Task<ApiResponse<LicenseDetailsView>> Handle(GetLicenseDrivingViewQuery request, CancellationToken cancellationToken)
        {
            var result = await _localDrivingLicenseApplicationServices.GetLicenseViewAsync(request.ApplicationId, request.LicenseId);

            if (!result.IsSuccess)
                return ApiResponseHandler.NotFound<LicenseDetailsView>("");

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
