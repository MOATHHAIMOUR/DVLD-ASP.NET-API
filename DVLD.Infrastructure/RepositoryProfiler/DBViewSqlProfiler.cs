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

            CreateMap<IDataRecord, TestAppointmentDetailInfo>()
               .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => (int)src["LocalDrivingLicenseApplicationId"]))
               .ForMember(dest => dest.PassedTests, opt => opt.MapFrom(src => (int)src["PassedTests"]))
               .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src["ClassName"].ToString()))
               .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => (int)src["ApplicationId"]))
               .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src["ApplicationStatus"].ToString()))
               .ForMember(dest => dest.PaidFees, opt => opt.MapFrom(src => (decimal)src["PaidFees"]))
               .ForMember(dest => dest.ApplicationTypeTitle, opt => opt.MapFrom(src => src["ApplicationTypeTitle"].ToString()))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src["FirstName"].ToString()))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src["LastName"].ToString()))
               .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src => (DateTime)src["ApplicationDate"]))
               .ForMember(dest => dest.LastStatusDate, opt => opt.MapFrom(src => (DateTime)src["LastStatusDate"]))
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src["Username"].ToString()));


            CreateMap<IDataRecord, LicenseDetailsView>()
            .ForMember(dest => dest.LicenseClassId, opt => opt.MapFrom(src => Enum.Parse<EnumLicenseClass>(src["LicenseClassId"].ToString()!)))
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src["ClassName"]))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src["FullName"]))
            .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src["LocalDrivingLicenseApplicationId"]))
            .ForMember(dest => dest.LicenseId, opt => opt.MapFrom(src => src["LicenseId"]))
            .ForMember(dest => dest.NationalNo, opt => opt.MapFrom(src => src["NationalNo"]))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (EnumGender)src["Gender"]))
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src["IssueDate"]))
            .ForMember(dest => dest.IssueReason, opt => opt.MapFrom(src => src["IssueReason"]))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src["Notes"]))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src["IsActive"]))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src["DateOfBirth"]))
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src["DriverId"]))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src["ExpirationDate"])).
             ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src["ImagePath"]))
            .ForMember(dest => dest.IsDetain, opt => opt.MapFrom(src => src["IsDetain"]))
            .ForMember(dest => dest.FineFees, opt => opt.MapFrom(src => src["FineFees"]));


            CreateMap<IDataRecord, TestAppointmentView>()
         .ForMember(dest => dest.TestAppointmentId,
                    opt => opt.MapFrom(src => src["TestAppointmentId"] != DBNull.Value
                                             ? Convert.ToInt32(src["TestAppointmentId"])
                                             : 0)) // Default value
         .ForMember(dest => dest.AppointmentDate,
                    opt => opt.MapFrom(src => src["AppointmentDate"] != DBNull.Value
                                             ? Convert.ToDateTime(src["AppointmentDate"])
                                             : DateTime.MinValue)) // Default value
         .ForMember(dest => dest.PaidFees,
                    opt => opt.MapFrom(src => src["PaidFees"] != DBNull.Value
                                             ? Convert.ToDecimal(src["PaidFees"])
                                             : 0.0m)) // Default value
         .ForMember(dest => dest.IsLocked,
                    opt => opt.MapFrom(src => src["IsLocked"] != DBNull.Value
                                             ? Convert.ToBoolean(src["IsLocked"])
                                             : false)) // Default value
.ForMember(dest => dest.TestResult,
           opt => opt.MapFrom(src => src["TestResult"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(src["TestResult"])));



            CreateMap<IDataRecord, ScheduleAndTake_TestView>()
                .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("LocalDrivingLicenseApplicationId"))))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("FirstName"))))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("LastName"))))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ClassName"))))
                .ForMember(dest => dest.TestTypeFees, opt => opt.MapFrom(src => src.GetDecimal(src.GetOrdinal("TestTypeFees"))))
                .ForMember(dest => dest.Tries, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("Tries"))));


            CreateMap<IDataRecord, TestAppointmentDetailInfo>()
                .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("LocalDrivingLicenseApplicationId"))))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ClassName"))))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("ApplicationId"))))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => (EnumApplicationStatus)src.GetByte(src.GetOrdinal("ApplicationStatus"))))
                .ForMember(dest => dest.PassedTests, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("PassedTests"))))
                .ForMember(dest => dest.PaidFees, opt => opt.MapFrom(src => src.GetDecimal(src.GetOrdinal("PaidFees"))))
                .ForMember(dest => dest.ApplicationTypeTitle, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("ApplicationTypeTitle"))))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("FirstName"))))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("LastName"))))
                .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src => src.GetDateTime(src.GetOrdinal("ApplicationDate"))))
                .ForMember(dest => dest.LastStatusDate, opt => opt.MapFrom(src => src.GetDateTime(src.GetOrdinal("LastStatusDate"))))
                                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.GetInt32(src.GetOrdinal("PersonId"))))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.GetString(src.GetOrdinal("Username"))));



        }
    }
}
