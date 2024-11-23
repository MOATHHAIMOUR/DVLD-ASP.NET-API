using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Queries.GetAllApplicationTypes
{
    public class GetAllApplicationTypesQueryHandler : IRequestHandler<GetAllApplicationTypesQuery, ApiResponse<List<ApplicationType>>>
    {
        private readonly ISharedRepository _sharedServices;

        public GetAllApplicationTypesQueryHandler(ISharedRepository sharedServices)
        {
            _sharedServices = sharedServices;
        }

        public async Task<ApiResponse<List<ApplicationType>>> Handle(GetAllApplicationTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _sharedServices.GetAllApplicationTypesAsync();

            return ApiResponseHandler.Success(result);
        }
    }
}
