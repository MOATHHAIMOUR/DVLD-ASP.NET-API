
using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;

namespace DVLD.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        public async Task<Result<User?>> GetUserByIdOrUserNameAsync(int? UserId, string? UserName)
        {
            User? User = await _userRepository.GetUserByIdOrUserName(UserId, UserName);

            return User == null
                ? Result<User?>.Failure(Error.RecoredNotFound("User not found"))
                : Result<User?>.Success(User);
        }

        public async Task<Result<int>> AddUserAsync(User User)
        {
            int insertedId = await _userRepository.AddUserAsycn(User);

            return Result<int>.Success(insertedId);
        }

        public async Task<Result<string>> DeleteUserByIdAsync(int UserId)
        {
            bool result = await _userRepository.DeleteUserAsync(UserId);

            return result ?
                Result<string>.Success($"User with ID {UserId} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID UserID."));
        }

        public async Task<Result<string>> UpdateUserAsync(User User)
        {
            bool result = await _userRepository.UpdateUserAsycn(User);

            return result ?
                Result<string>.Success($"User with id: {User.UserId} is Updated successfully")
                :
                Result<string>.Failure(Error.RecoredNotFound($"user was not found"));
        }

        public Task<List<User>> GetUsers(int? userId = null, int? personId = null, string? userName = null, bool? isActive = null, string? sortBy = null, EnumSortDirection? sortDirection = EnumSortDirection.ASC, int? pageSize = 10, int? pageNumber = 1)
        {
            return _userRepository.GetUsers(userId, personId, userName, isActive, sortBy, sortDirection, pageSize, pageNumber);
        }


    }
}
