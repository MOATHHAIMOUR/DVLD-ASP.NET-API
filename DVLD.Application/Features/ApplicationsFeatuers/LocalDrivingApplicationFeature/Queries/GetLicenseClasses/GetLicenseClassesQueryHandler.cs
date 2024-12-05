using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseClasses
{
    public class GetLicenseClassesQueryHandler : IRequestHandler<GetLicenseClassesQuery, ApiResponse<IEnumerable<GetLicenseClassDTO>>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingApplicationServices;
        private readonly IMapper _mapper;

        public GetLicenseClassesQueryHandler(ILocalDrivingLicenseApplicationServices localDrivingApplicationServices, IMapper mapper)
        {
            _localDrivingApplicationServices = localDrivingApplicationServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetLicenseClassDTO>>> Handle(GetLicenseClassesQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = await _localDrivingApplicationServices.GetLicenseClassesAsync();

            // Handle null Value gracefully
            var licenseClasses = apiResponse.Value ?? [];

            // Map the entities to DTOs
            var mappedResult = _mapper.Map<IEnumerable<GetLicenseClassDTO>>(licenseClasses);

            // Return the mapped result
            return ApiResponseHandler.Success(mappedResult);
        }

    }
}
