using AutoMapper;
using DVLD.Domain.views.Test;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class TestSqlProfiler : Profile
    {
        public TestSqlProfiler()
        {
            CreateMap<IDataRecord, ScheduleAndTake_TestView>()
                .ForMember(dest => dest.LocalDrivingLicenseApplicationId,
                           opt => opt.MapFrom(src => src["LocalDrivingLicenseApplicationId"] != DBNull.Value
                                                    ? Convert.ToInt32(src["LocalDrivingLicenseApplicationId"])
                                                    : 0)) // Default value
                .ForMember(dest => dest.TestAppointmentId,
                           opt => opt.MapFrom(src => src["TestAppointmentId"] != DBNull.Value
                                                    ? Convert.ToInt32(src["TestAppointmentId"])
                                                    : 0)) // Default value
                .ForMember(dest => dest.FirstName,
                           opt => opt.MapFrom(src => src["FirstName"] != DBNull.Value
                                                    ? src["FirstName"].ToString()
                                                    : string.Empty))
                .ForMember(dest => dest.LastName,
                           opt => opt.MapFrom(src => src["LastName"] != DBNull.Value
                                                    ? src["LastName"].ToString()
                                                    : string.Empty))
                .ForMember(dest => dest.ClassName,
                           opt => opt.MapFrom(src => src["ClassName"] != DBNull.Value
                                                    ? src["ClassName"].ToString()
                                                    : string.Empty))
                .ForMember(dest => dest.TestTypeFees,
                           opt => opt.MapFrom(src => src["TestTypeFees"] != DBNull.Value
                                                    ? Convert.ToDecimal(src["TestTypeFees"])
                                                    : 0.0m)) // Default value
                .ForMember(dest => dest.Tries,
                           opt => opt.MapFrom(src => src["Tries"] != DBNull.Value
                                                    ? Convert.ToInt32(src["Tries"])
                                                    : 0)); // Default value





        }
    }
}
