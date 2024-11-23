
namespace DVLD.Domain.Entites
{
    public class User
    {
        public required int UserId { get; set; }
        public int PersonId { get; set; }
        public required string UserName { get; set; }
        public string? Password { get; set; }
        public required bool IsActive { get; set; }

    }
}
