using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddLocalLicenses
{
    public class AddLocalLicenseCommandHandler : IRequestHandler<AddLocalLicenseCommand, ApiResponse<string>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingLicenseApplicationServices;
        private readonly IMapper _mapper;

        public AddLocalLicenseCommandHandler(ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices, IMapper mapper)
        {
            _localDrivingLicenseApplicationServices = localDrivingLicenseApplicationServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddLocalLicenseCommand request, CancellationToken cancellationToken)
        {
            // Map the incoming DTO to the License entity
            var license = _mapper.Map<License>(request.AddNewDetainLicenseDTO);

            // Call the service to add the license and await the result
            var result = await _localDrivingLicenseApplicationServices.AddLicensesForFirstTimeAsync(license);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<string>([result.Error.Message]);

            // Return a success response with the newly created license ID
            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
