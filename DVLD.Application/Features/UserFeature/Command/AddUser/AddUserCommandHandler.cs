
using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApiResponse<string>>
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.AddUserDTO);

            var result = await _userServices.AddUserAsync(user);


            return result.IsSuccess ?
                 ApiResponseHandler.Success($"User with Id: {result.Value} has been successfully added to the system")
                 :
                 ApiResponseHandler.NotFound<string>([result.Error.Message]);
        }
    }
}