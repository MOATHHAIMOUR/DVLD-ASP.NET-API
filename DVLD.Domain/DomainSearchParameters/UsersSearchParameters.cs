namespace DVLD.Domain.DomainSearchParameters
{
    public class UsersSearchParameters
    {
        public int? UserId { get; set; }
        public int? PersonId { get; set; }
        public string? UserName { get; set; }
        public bool? IsActive { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}
