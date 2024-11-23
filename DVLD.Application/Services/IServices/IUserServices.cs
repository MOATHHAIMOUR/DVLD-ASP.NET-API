
using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;

namespace DVLD.Application.Services.IServices
{
    public interface IUserServices
    {
        public Task<List<User>> GetUsers(int? userId = null, int? personId = null, string? userName = null, bool? isActive = null, string? sortBy = null, EnumSortDirection? sortDirection = EnumSortDirection.ASC, int? pageSize = 10, int? pageNumber = 1);

        public Task<Result<User?>> GetUserByIdOrUserNameAsync(int? userId, string? UserName);

        public Task<Result<string>> DeleteUserByIdAsync(int userId);

        public Task<Result<int>> AddUserAsync(User user);

        public Task<Result<string>> UpdateUserAsync(User user);
    }
}
