using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseClasses
{
    public class GetLicenseClassesQuery : IRequest<ApiResponse<IEnumerable<GetLicenseClassDTO>>>
    {

    }
}
