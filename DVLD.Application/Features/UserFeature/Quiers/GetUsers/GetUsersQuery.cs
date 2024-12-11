using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using DVLD.Domain.DomainSearchParameters;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUsersQuery : IRequest<ApiResponse<IEnumerable<GetUserDTO>>>
    {
        public UsersSearchParameters UsersSearchParameters { get; set; }
        public GetUsersQuery(UsersSearchParameters userSearchParamsDTO)
        {
            UsersSearchParameters = userSearchParamsDTO;
        }

    }
}
