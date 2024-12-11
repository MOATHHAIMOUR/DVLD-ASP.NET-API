
using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;

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
            // cheek if user exist 
            var IsPersonUser = await _userRepository.IsExist(UserStoredPeocedures.IsUserExistsByPersonId, "PersonId", User.PersonId.ToString());

            if (IsPersonUser)
                return Result<int>.Failure(Error.ValidationError("Current person is already a user in system"));

            int insertedId = await _userRepository.AddAsync(UserStoredPeocedures.SP__AddUser, User);

            return Result<int>.Success(insertedId);
        }

        public async Task<Result<string>> DeleteUserByIdAsync(int UserId)
        {
            bool result = await _userRepository.DeleteAsync(UserStoredPeocedures.SP_DeleteUserById, "UserId", UserId);

            return result ?
                Result<string>.Success($"User with ID {UserId} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID UserID."));
        }

        public async Task<Result<string>> UpdateUserAsync(User User)
        {
            bool result = await _userRepository.UpdateAsync(UserStoredPeocedures.SP_UpdateUser, User);

            return result ?
                Result<string>.Success($"User with id: {User.UserId} is Updated successfully")
                :
                Result<string>.Failure(Error.RecoredNotFound($"user was not found"));
        }

        public async Task<Result<IEnumerable<User>>> GetUsers(UsersSearchParameters userSearchParamsDTO)
        {
            return Result<IEnumerable<User>>.Success(await _userRepository.GetFilterdUsersAsync(UserStoredPeocedures.SP_GetUsers, userSearchParamsDTO));
        }


    }
}
