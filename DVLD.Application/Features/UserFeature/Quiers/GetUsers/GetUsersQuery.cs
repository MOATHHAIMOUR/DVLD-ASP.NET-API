using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUsersQuery : IRequest<ApiResponse<List<GetUserDTO>>>
    {
        public UserSearchParamsDTO UserSearchParamsDTO { get; set; }
        public GetUsersQuery(UserSearchParamsDTO userSearchParamsDTO)
        {
            UserSearchParamsDTO = userSearchParamsDTO;
        }

    }
}
