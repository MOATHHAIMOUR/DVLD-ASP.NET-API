using DVLD.Domain.Enums;

namespace DVLD.Domain.Entites
{
    public class LocalDrivingLicenseApplication
    {
        public required int LocalDrivingLicenseApplicationId { get; set; }
        public required int ApplicationId { get; set; }
        public required EnumLicenseClass LicenseClassId { get; set; }


        // composition properties 
        public Application Application { get; set; } = new Application();
        public LicenseClass LicenseClass { get; set; } = new LicenseClass();
    }

}