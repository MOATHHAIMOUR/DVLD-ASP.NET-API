﻿namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class GetLicenseClassDTO
    {
        public int LicenseClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string ClassDescription { get; set; } = string.Empty;
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
    }
}
