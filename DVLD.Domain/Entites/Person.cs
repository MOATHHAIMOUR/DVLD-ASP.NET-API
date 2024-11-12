using DVLD.Domain.Enums;

namespace DVLD.Domain.Entites
{
    public class Person
    {
        public required int PersonId { get; set; }        // Primary Key
        public required string NationalNo { get; set; }   // National Number
        public required string FirstName { get; set; }    // First Name
        public string? SecondName { get; set; }   // Second Name
        public string? ThirdName { get; set; }    // Third Name
        public required string LastName { get; set; }     // Last Name
        public required EnumGender Gender { set; get; }      // Gender (e.g., 'M' or 'F')
        public required string Phone { get; set; }        // Phone
        public required string Email { get; set; }        // Email
        public required int CountryId { get; set; }       // Foreign Key to Country
        public required string Address { set; get; }
        public required DateTime DateOfBirth { set; get; }
        public byte[]? Image { get; set; }        // Image

        // Navigation property for the relationship with Country
        public Country? Country { get; set; }

        // Navigation property to relate with Users
        public User? User { get; set; }
    }
}