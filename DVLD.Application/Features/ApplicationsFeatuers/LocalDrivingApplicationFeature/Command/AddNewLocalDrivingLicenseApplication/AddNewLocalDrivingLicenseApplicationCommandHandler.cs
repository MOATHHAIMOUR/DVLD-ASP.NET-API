using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicenseApplication
{
    public class AddNewLocalDrivingLicenseCommandHandler : IRequestHandler<AddNewLocalDrivingLicenseApplicationCommandHandler, ApiResponse<string>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingApplicationServices;
        private readonly IMapper _mapper;

        public AddNewLocalDrivingLicenseCommandHandler(ILocalDrivingLicenseApplicationServices localDrivingApplicationServices, IMapper mapper)
        {
            _localDrivingApplicationServices = localDrivingApplicationServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddNewLocalDrivingLicenseApplicationCommandHandler request, CancellationToken cancellationToken)
        {
            LocalDrivingLicenseApplication localDrivingLicenseApplication = _mapper.Map<LocalDrivingLicenseApplication>(request.AddNewLocalDrivingLicenseDTO);

            var result = await _localDrivingApplicationServices.AddNewLocalDrivingLicenseApplication(localDrivingLicenseApplication);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<string>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);

        }


    }
}

