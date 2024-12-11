
using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;

namespace DVLD.Application.Services.IServices
{
    public interface IUserServices
    {
        public Task<Result<IEnumerable<User>>> GetUsers(UsersSearchParameters userSearchParamsDTO);

        public Task<Result<User?>> GetUserByIdOrUserNameAsync(int? userId, string? UserName);

        public Task<Result<string>> DeleteUserByIdAsync(int userId);

        public Task<Result<int>> AddUserAsync(User user);

        public Task<Result<string>> UpdateUserAsync(User user);
    }
}
