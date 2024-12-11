using DVLD.Domain.Enums;

namespace DVLD.Domain.Entites
{
    public class Person
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
        public required string Address { set; get; }
        public required DateTime DateOfBirth { set; get; }
        public required string ImagePath { get; set; }
        public required bool IsUser { get; set; }



        // Navigation property to relate with Users
        public Country? Country { get; set; }
        public User? User { get; set; }
    }
}