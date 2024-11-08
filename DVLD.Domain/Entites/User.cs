namespace DVLD.Domain.Entites
{
    public class User
    {
        public required int UserId { get; set; }          // Primary Key
        public required int PersonId { get; set; }        // Foreign Key to Person
        public required string Username { get; set; }     // Username
        public required string Password { get; set; }     // Password
        public required bool IsActive { get; set; }       // Active status

        // Navigation property to relate with Person
        public Person Person { get; set; }
    }
}