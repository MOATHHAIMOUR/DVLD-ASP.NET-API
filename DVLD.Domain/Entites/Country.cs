namespace DVLD.Domain.Entites
{
    public class Country
    {
        public int CountryId { get; set; }  // Primary Key
        public required string Name { get; set; }    // Country Name

        // Navigation property to relate with People
        public List<Person> People { get; set; }
    }
}