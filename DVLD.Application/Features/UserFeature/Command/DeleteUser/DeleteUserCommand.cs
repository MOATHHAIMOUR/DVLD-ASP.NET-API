
using DVLD.Application.Common.ApiResponse;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<ApiResponse<string>>
    {
        public int UserId { get; set; }
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
