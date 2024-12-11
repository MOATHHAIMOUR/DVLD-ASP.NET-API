
using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<string>>
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.UpdateUserDTO);

            var result = await _userServices.UpdateUserAsync(user);

            return result.IsSuccess ?
                 ApiResponseHandler.Success(result.Value!)
                 :
                 ApiResponseHandler.NotFound<string>([result.Error.Message]);
        }
    }
}