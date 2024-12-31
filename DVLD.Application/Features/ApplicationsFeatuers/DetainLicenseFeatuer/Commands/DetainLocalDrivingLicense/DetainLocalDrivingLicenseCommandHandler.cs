using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.DetainLocalDrivingLicense
{
    public class DetainLocalDrivingLicenseCommandHandler : IRequestHandler<DetainLocalDrivingLicenseCommand, ApiResponse<object>>
    {
        private readonly IDetainLicenseServices _detainLicenseServices;
        private readonly IMapper _mapper;

        public DetainLocalDrivingLicenseCommandHandler(IDetainLicenseServices detainLicenseServices, IMapper mapper)
        {
            _detainLicenseServices = detainLicenseServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(DetainLocalDrivingLicenseCommand request, CancellationToken cancellationToken)
        {
            DetainedLicense detainedLicense = _mapper.Map<DetainedLicense>(request.DetainLicenseDTO);

            var detainLocalLicenseResultDTO = await _detainLicenseServices.DetainLocalDrivingLicenseAsync(detainedLicense);

            if (!detainLocalLicenseResultDTO.IsSuccess)
                return ApiResponseHandler.BadRequest<object>([detainLocalLicenseResultDTO.Error.Message]);


            return ApiResponseHandler.Success<object>(new
            {
                detainLicenseId = detainLocalLicenseResultDTO.Value
            });
        }
    }
}
