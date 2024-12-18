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
        private readonly ISharedServices _sharedServices;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserServices userServices, ISharedServices sharedServices, IMapper mapper)
        {
            _userServices = userServices;
            _sharedServices = sharedServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetUserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserDTO>>((await _userServices.GetUsers(request.UsersSearchParameters)).Value ?? []);

            int totalUsers = (await _sharedServices.GetRowCountAsync("Users")).Value;

            int totalPages = (int)Math.Ceiling(totalUsers / Convert.ToDouble(request.UsersSearchParameters.PageSize));

            return ApiResponseHandler.Success(usersDto, meta: new
            {
                count = totalUsers,
                totalPages,
            });

        }
    }
}
