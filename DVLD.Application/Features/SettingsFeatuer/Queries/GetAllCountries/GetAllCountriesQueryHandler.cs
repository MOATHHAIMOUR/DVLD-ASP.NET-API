using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Features.SettingsFeatuer.Queries.GetAllCountries;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllCountries
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountryQuery, ApiResponse<List<Country>>>
    {
        private readonly ISharedServices _countryServices;

        public GetAllCountriesQueryHandler(ISharedServices countryServices)
        {
            _countryServices = countryServices;
        }

        async Task<ApiResponse<List<Country>>> IRequestHandler<GetAllCountryQuery, ApiResponse<List<Country>>>.Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var result = await _countryServices.GetAllCountriesAsync();

            return ApiResponseHandler.Success(result.Value ?? []);
        }
    }
}
