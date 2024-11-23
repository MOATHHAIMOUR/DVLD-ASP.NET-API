namespace DVLD.Application.DTO.Users
{
    public class UpdateUserDTO
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required bool IsActive { get; set; }

    }
}
