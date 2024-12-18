namespace DVLD.Domain.DomainSearchParameters
{
    public class UsersSearchParameters
    {
        public int? UserId { get; set; }
        public int? PersonId { get; set; }
        public string? UserName { get; set; }
        public bool? IsActive { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}
