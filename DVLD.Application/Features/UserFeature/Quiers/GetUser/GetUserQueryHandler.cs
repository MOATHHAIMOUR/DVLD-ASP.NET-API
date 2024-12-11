
using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApiResponse<GetUserDTO>>
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetUserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userServices.GetUserByIdOrUserNameAsync(request.UserId, request.UserName);

            return result.IsSuccess ?
                ApiResponseHandler.Success(_mapper.Map<GetUserDTO>(result.Value)) :
                ApiResponseHandler.NotFound<GetUserDTO>([result.Error.Message]);
        }
    }
}