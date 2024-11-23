namespace DVLD.Application.DTO.Users
{
    public class UserSearchParamsDTO
    {
        public int? UserId { get; set; }
        public int? PersonId { get; set; }
        public string? UserName { get; set; }
        public bool? IsActive { get; set; }
        public string? SortBy { get; set; }
        public string? SortDireaction { get; set; }
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}
