using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicense
{
    public class AddNewLocalDrivingLicenseCommandHandler : IRequestHandler<AddNewLocalDrivingLicenseCommand, ApiResponse<string>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingApplicationServices;
        private readonly IMapper _mapper;

        public AddNewLocalDrivingLicenseCommandHandler(ILocalDrivingLicenseApplicationServices localDrivingApplicationServices, IMapper mapper)
        {
            _localDrivingApplicationServices = localDrivingApplicationServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddNewLocalDrivingLicenseCommand request, CancellationToken cancellationToken)
        {
            LocalDrivingLicenseApplication localDrivingLicenseApplication = _mapper.Map<LocalDrivingLicenseApplication>(request.AddNewLocalDrivingLicenseDTO);

            var result = await _localDrivingApplicationServices.AddNewLocalDrivingLicense(localDrivingLicenseApplication);

            if (result.IsSuccess)
                return ApiResponseHandler.Success(result.Value!);

            return ApiResponseHandler.BadRequest<string>([

             result.Error.Message,
  ]);
        }
    }
}

