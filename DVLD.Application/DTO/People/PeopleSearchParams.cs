using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.People
{
    public class PeopleSearchParams
    {
        public int? PersonId { get; set; }
        public string? NationalNo { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CountryName { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }

        

    }
}
