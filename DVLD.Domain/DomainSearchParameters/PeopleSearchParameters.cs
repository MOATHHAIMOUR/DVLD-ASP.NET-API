﻿using DVLD.Domain.Enums;

namespace DVLD.Domain.DomainSearchParameters
{
    public class PeopleSearchParameters
    {
        public int? PersonId { get; set; }
        public string? NationalNo { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? LastName { get; set; }
        public EnumGender? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CountryName { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "ASC";
    }
}
