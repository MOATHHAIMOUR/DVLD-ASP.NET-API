using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.License.LocalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView
{
    public class GetLicenseDrivingViewQuery : IRequest<ApiResponse<LicenseDetailsView>>
    {
        public GetLicenseDrivingViewQuery(int? applicationId, int? licenseId, int? localDrivingApplicationId)
        {
            ApplicationId = applicationId;
            LicenseId = licenseId;
            LocalDrivingApplicationId = localDrivingApplicationId;
        }

        public int? ApplicationId { get; set; }
        public int? LicenseId { get; set; }
        public int? LocalDrivingApplicationId { get; set; }

    }
}
