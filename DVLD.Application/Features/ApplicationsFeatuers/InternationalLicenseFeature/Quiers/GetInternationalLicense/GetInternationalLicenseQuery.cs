using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.License.InternationalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Quiers.GetInternationalLicense
{
    public class GetInternationalLicenseQuery : IRequest<ApiResponse<InternationalLicenseView>>
    {
        public GetInternationalLicenseQuery(int internationalLicenseId)
        {
            InternationalLicenseId = internationalLicenseId;
        }

        public int InternationalLicenseId { get; set; }
    }
}
