using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUser
{
    public class GetUserQuery : IRequest<ApiResponse<GetUserDTO>>
    {
        public GetUserQuery(int? id, string? userName)
        {
            UserId = id;
            UserName = userName;
        }

        public int? UserId { get; set; }
        public string? UserName { get; set; }

    }
}
