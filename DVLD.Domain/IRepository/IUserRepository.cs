using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
namespace DVLD.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdOrUserName(int? UserId, string? UserName);

        Task<int> AddUserAsycn(User User);

        Task<bool> UpdateUserAsycn(User user);

        Task<bool> DeleteUserAsync(int userId);

        Task<List<User>> GetUsers(int? userId = null, int? personId = null, string? userName = null, bool? isActive = null, string? sortBy = null, EnumSortDirection? sortDirection = EnumSortDirection.ASC, int? pageSize = 10, int? pageNumber = 1);
    }
}
