using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<IEnumerable<GetUserDTO>>>
    {
        private readonly IUserServices _userServices;

        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetUserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserDTO>>((await _userServices.GetUsers(request.UsersSearchParameters)).Value ?? []);

            return ApiResponseHandler.Success(usersDto);

        }
    }
}
