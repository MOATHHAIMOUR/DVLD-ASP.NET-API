using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Quiers.GetAllInternationalLicense
{
    public class GetAllInternationalLicenseQuery : IRequest<ApiResponse<IEnumerable<GetInternationalLicenseDTO>>>
    {

    }
}
