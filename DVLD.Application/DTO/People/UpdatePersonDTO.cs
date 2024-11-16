namespace DVLD.Application.DTO.People
{
    public class UpdatePersonDTO
    {
        public required int PersonId { get; set; }  
        public required string NationalNo { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { set; get; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required int CountryId { get; set; }
        public required string Address { set; get; }
        public required DateTime DateOfBirth { set; get; }
    }
}
