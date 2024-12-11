using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userServices.DeleteUserByIdAsync(request.UserId);

            return result.IsSuccess ?
                 ApiResponseHandler.Success(result.Value!)
                 :
                 ApiResponseHandler.NotFound<string>([result.Error.Message]);
        }
    }
}