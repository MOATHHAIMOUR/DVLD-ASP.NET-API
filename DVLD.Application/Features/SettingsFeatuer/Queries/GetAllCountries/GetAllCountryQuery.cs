using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Queries.GetAllCountries
{
    public class GetAllCountryQuery : IRequest<ApiResponse<List<Country>>>
    {
    }
}
