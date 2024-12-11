using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.ReleaseLocalDrivingLicense
{
    public class ReleaseLocalDrivingLicenseCommandHandler : IRequestHandler<ReleaseLocalDrivingLicenseCommand, ApiResponse<object>>
    {
        private readonly IDetainLicenseServices _detainLicenseServices;
        private readonly IMapper _mapper;

        public ReleaseLocalDrivingLicenseCommandHandler(IDetainLicenseServices detainLicenseServices, IMapper mapper)
        {
            _detainLicenseServices = detainLicenseServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(ReleaseLocalDrivingLicenseCommand request, CancellationToken cancellationToken)
        {

            var result = await _detainLicenseServices.ReleaseDetainLocalDrivingLicenseAsync(request.LicenseReleaseDTO);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<object>([result.Error.Message]);


            return ApiResponseHandler.Success<object>(new
            {
                ReleaseApplicationId = result.Value
            });
        }
    }
}
