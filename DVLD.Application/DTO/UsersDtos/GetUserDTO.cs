
namespace DVLD.Application.DTO.Users
{
    public class GetUserDTO
    {
        public required int UserId { get; set; }
        public int? PersonId { get; set; }
        public required string Username { get; set; }
        public required bool IsActive { get; set; }

    }
}
