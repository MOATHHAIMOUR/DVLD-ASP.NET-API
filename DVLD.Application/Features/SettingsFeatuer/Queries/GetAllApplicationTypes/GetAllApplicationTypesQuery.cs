using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Queries.GetAllApplicationTypes
{
    public class GetAllApplicationTypesQuery : IRequest<ApiResponse<List<ApplicationType>>>
    {
    }
}
