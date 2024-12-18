using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Queries.GetLicenseClasses
{
    public class GetLicenseClassesQuery : IRequest<ApiResponse<IEnumerable<GetLicenseClassDTO>>>
    {

    }
}
