using DVLD.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DVLD.Application.DTO.PersonDtos
{
    public class UpdatePersonDTO
    {
        public required int PersonId { get; set; }
        public required string NationalNo { get; set; }
        public required string FirstName { get; set; }
        public required string SecondName { get; set; }
        public required string ThirdName { get; set; }
        public required string LastName { get; set; }
        public required EnumGender Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required int CountryId { get; set; }
        public required string Address { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }

    }
}
