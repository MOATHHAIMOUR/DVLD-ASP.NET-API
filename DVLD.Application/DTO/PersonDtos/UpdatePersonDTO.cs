namespace DVLD.Application.DTO.PersonDtos
{
    public class UpdatePersonDTO
    {
        public required int PersonId { get; set; }

        public required string NationalNo { get; set; }


        public required string FirstName { get; set; }


        public string? SecondName { get; set; }


        public string? ThirdName { get; set; }


        public required string LastName { get; set; }


        public required string Gender { get; set; }


        public required string Phone { get; set; }


        public required string Email { get; set; }


        public required int CountryId { get; set; }


        public required string Address { get; set; }

        public string? ImagePath { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }
}
