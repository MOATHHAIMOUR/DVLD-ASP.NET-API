using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.License.LocalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView
{
    public class GetLicenseDrivingViewQuery : IRequest<ApiResponse<LicenseDetailsView>>
    {
        public GetLicenseDrivingViewQuery(int? applicationId, int? licenseId)
        {
            ApplicationId = applicationId;
            LicenseId = licenseId;
        }

        public int? ApplicationId { get; set; }
        public int? LicenseId { get; set; }

    }
}
