using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Command.UpdateApplicationType
{
    public class UpdateApplicationTypeCommandHandler : IRequestHandler<UpdateApplicationTypeCommand, ApiResponse<string>>
    {
        private readonly ISharedServices _sharedServices;
        private readonly IMapper _mapper;

        public UpdateApplicationTypeCommandHandler(ISharedServices sharedServices, IMapper mapper)
        {
            _sharedServices = sharedServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateApplicationTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _sharedServices.UpdateApplicationType(_mapper.Map<ApplicationType>(request.UpdateApplicationTypeDTO));

            if (result.IsSuccess)
            {
                return ApiResponseHandler.Success(result.Value!);
            }
            else
            {
                return ApiResponseHandler.NotFound<string>(result.Error.Message);
            }

        }
    }
}
