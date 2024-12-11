using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;
namespace DVLD.Domain.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserByIdOrUserName(int? UserId, string? UserName);

        public Task<IEnumerable<User>> GetFilterdUsersAsync(string storedProcedure, UsersSearchParameters usersSearchParameters);

    }
}
