using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.TestDTOs
{
    public class SearchTestAppointmentViewDTO
    {
        public int? LocalDrivingLicenseApplicationId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public EnumTestType? TestTypeId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? TestResult { get; set; }
        public bool? IsLocked { get; set; }
        public string? AppointmentCreatedByUser { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "ASC";
        public int? PageSize { get; set; } = 10;
        public int? PageNumber { get; set; } = 1;
    }
}
