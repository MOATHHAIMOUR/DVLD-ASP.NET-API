using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.PersonDtos
{
    public class GetPersonDTO
    {
        public required int PersonId { get; set; }
        public required string NationalNo { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public required string LastName { get; set; }
        public required EnumGender Gender { set; get; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required int CountryId { get; set; }
        public required string CountryName { get; set; }
        public required string Address { set; get; }
        public required DateTime DateOfBirth { set; get; }
        public required bool IsUser { set; get; }
        public required string ImagePath { get; set; }
    }
}
