using DVLD.Domain.Enums;

namespace DVLD.Domain.Entites
{
    public class Person
    {
        public int PersonId { get; set; }        // Primary Key
        public string NationalNo { get; set; }   // National Number
        public string FirstName { get; set; }    // First Name
        public string SecondName { get; set; }   // Second Name
        public string ThirdName { get; set; }    // Third Name
        public string LastName { get; set; }     // Last Name
        public char Gender { set; get; }      // Gender (e.g., 'M' or 'F')
        public string Phone { get; set; }        // Phone
        public string Email { get; set; }        // Email
        public int CountryId { get; set; }       // Foreign Key to Country
        public byte[] Image { get; set; }        // Image

        // Navigation property for the relationship with Country
        public Country Country { get; set; }

        // Navigation property to relate with Users
        public User User { get; set; }
    }
}