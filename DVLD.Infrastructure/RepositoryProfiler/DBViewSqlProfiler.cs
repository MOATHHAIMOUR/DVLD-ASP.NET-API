using AutoMapper;
using DVLD.Domain.Enums;
using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.Test;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class DBViewSqlProfiler : Profile
    {
        public DBViewSqlProfiler()
        {
            CreateMap<IDataRecord, InternationalLicenseView>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src["FullName"].ToString()))
           .ForMember(dest => dest.InternationalLicenseId, opt => opt.MapFrom(src => (int)src["InternationalLicenseId"]))
           .ForMember(dest => dest.NationalNo, opt => opt.MapFrom(src => src["NationalNo"].ToString()))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (EnumGender)src["Gender"]))
           .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => (DateTime)src["IssueDate"]))
           .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => (int)src["ApplicationId"]))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => (bool)src["IsActive"]))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => (DateTime)src["DateOfBirth"]))
           .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => (int)src["DriverId"]))
           .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => (DateTime)src["ExpirationDate"]));


            CreateMap<IDataRecord, LicenseDetailsView>()
            .ForMember(dest => dest.LicenseClassId, opt => opt.MapFrom(src => Enum.Parse<EnumLicenseClass>(src["LicenseClassId"].ToString()!)))
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src["ClassName"]))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src["FullName"]))
            .ForMember(dest => dest.LicenseId, opt => opt.MapFrom(src => src["LicenseId"]))
            .ForMember(dest => dest.NationalNo, opt => opt.MapFrom(src => src["NationalNo"]))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (EnumGender)src["Gender"]))
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src["IssueDate"]))
            .ForMember(dest => dest.IssueReason, opt => opt.MapFrom(src => src["IssueReason"]))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src["Notes"]))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src["IsActive"]))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src["DateOfBirth"]))
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src["DriverId"]))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src["ExpirationDate"]))
            .ForMember(dest => dest.IsDetain, opt => opt.MapFrom(src => src["IsDetain"]));


            CreateMap<IDataRecord, TestAppointmentView>()
           .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src["LocalDrivingLicenseApplicationId"]))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src["FirstName"]))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src["LastName"]))
           .ForMember(dest => dest.TestTypeTitle, opt => opt.MapFrom(src => src["TestTypeTitle"]))
           .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src["AppointmentDate"]))
           .ForMember(dest => dest.TestResult, opt => opt.MapFrom(src => src["TestResult"]))
           .ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src["IsLocked"]))
           .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src["Notes"]))
           .ForMember(dest => dest.AppointmentCreatedByUser, opt => opt.MapFrom(src => src["AppointmentCreatedByUser"]));

            CreateMap<IDataRecord, ScheduleTestView>()
                .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("LocalDrivingLicenseApplicationId"))))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("FirstName"))))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("LastName"))))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ClassName"))))
                .ForMember(dest => dest.TestTypeFees, opt => opt.MapFrom(src => src.GetDecimal(src.GetOrdinal("TestTypeFees"))))
                .ForMember(dest => dest.Tries, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("Tries"))))
                .ForMember(dest => dest.TestAppointmentId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("TestAppointmentId"))));


            CreateMap<IDataRecord, TestLocalApplicationView>()
                .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("LocalDrivingLicenseApplicationId"))))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ClassName"))))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("ApplicationId"))))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => (EnumApplicationStatus)src.GetByte(src.GetOrdinal("ApplicationStatus"))))
                .ForMember(dest => dest.PassedTests, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("PassedTests"))))
                .ForMember(dest => dest.PaidFees, opt => opt.MapFrom(src => src.GetDecimal(src.GetOrdinal("PaidFees"))))
                .ForMember(dest => dest.ApplicationTypeTitle, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ApplicationTypeTitle"))))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("FullName"))))
                .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src => src.GetDateTime(src.GetOrdinal("ApplicationDate"))))
                .ForMember(dest => dest.LastStatusDate, opt => opt.MapFrom(src => src.GetDateTime(src.GetOrdinal("LastStatusDate"))))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("Username"))))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("PersonId"))));



        }
    }
}
