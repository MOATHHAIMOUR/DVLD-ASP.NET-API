using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Queries.GetLicenseClasses
{
    public class GetLicenseClassesQueryHandler : IRequestHandler<GetLicenseClassesQuery, ApiResponse<IEnumerable<GetLicenseClassDTO>>>
    {
        private readonly ISharedServices _sharedServices;
        private readonly IMapper _mapper;

        public GetLicenseClassesQueryHandler(ISharedServices sharedServices, IMapper mapper)
        {
            _sharedServices = sharedServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetLicenseClassDTO>>> Handle(GetLicenseClassesQuery request, CancellationToken cancellationToken)
        {
            var licensesClasses = (await _sharedServices.GetAllLicensesClassesAsync()).Value;

            // Map the entities to DTOs
            var mappedResult = _mapper.Map<IEnumerable<GetLicenseClassDTO>>(licensesClasses);

            // Return the mapped result
            return ApiResponseHandler.Success(mappedResult ?? []);
        }

    }
}
