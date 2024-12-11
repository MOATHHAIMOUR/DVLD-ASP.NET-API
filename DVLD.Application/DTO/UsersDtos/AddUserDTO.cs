
namespace DVLD.Application.DTO.Users
{
    public class AddUserDTO
    {
        public required int PersonId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required bool IsActive { get; set; }
    }
}
