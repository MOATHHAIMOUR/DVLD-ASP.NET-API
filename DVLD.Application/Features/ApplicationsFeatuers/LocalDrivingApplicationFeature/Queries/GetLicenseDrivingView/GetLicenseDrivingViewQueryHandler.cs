using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.License.LocalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView
{
    public class GetLicenseDrivingViewQueryHandler : IRequestHandler<GetLicenseDrivingViewQuery, ApiResponse<LicenseDetailsView>>
    {

        private readonly ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices;

        public GetLicenseDrivingViewQueryHandler(ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices)
        {
            this.localDrivingLicenseApplicationServices = localDrivingLicenseApplicationServices;
        }

        public async Task<ApiResponse<LicenseDetailsView>> Handle(GetLicenseDrivingViewQuery request, CancellationToken cancellationToken)
        {
            var result = await localDrivingLicenseApplicationServices.GetLicenseViewAsync(request.ApplicationId, request.LicenseId, request.LocalDrivingApplicationId);

            if (!result.IsSuccess)
                return ApiResponseHandler.NotFound<LicenseDetailsView>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
