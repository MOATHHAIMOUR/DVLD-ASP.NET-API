using DVLD.Domain.Enums;

namespace DVLD.Domain.views.Person
{
    public class PersonView
    {
        public int PersonId { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string LastName { get; set; }
        public EnumGender Gender { set; get; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { set; get; }
        public string DateOfBirth { set; get; }
        public string CountryName { get; set; }

    }
}