using AutoMapper;
using DVLD.Domain.Entites;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class LicenseSqlProfiler : Profile
    {
        public LicenseSqlProfiler()
        {
            CreateMap<IDataRecord, License>()
                .ForMember(dest => dest.LicenseId, opt => opt.MapFrom(src =>
                    src["LicenseId"] != DBNull.Value ? (int)src["LicenseId"] : default))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src =>
                    src["ApplicationId"] != DBNull.Value ? (int?)src["ApplicationId"] : null))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src =>
                    src["DriverId"] != DBNull.Value ? (int?)src["DriverId"] : null))
                .ForMember(dest => dest.LicenseClassId, opt => opt.MapFrom(src =>
                    src["LicenseClassId"] != DBNull.Value ? (int?)src["LicenseClassId"] : null))
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src =>
                    src["IssueDate"] != DBNull.Value ? (DateTime)src["IssueDate"] : default))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src =>
                    src["ExpirationDate"] != DBNull.Value ? (DateTime)src["ExpirationDate"] : default))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src =>
                    src["Notes"] != DBNull.Value ? src["Notes"].ToString() : string.Empty))
                .ForMember(dest => dest.PaidFees, opt => opt.MapFrom(src =>
                    src["PaidFees"] != DBNull.Value ? (decimal?)src["PaidFees"] : null))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src =>
                    src["IsActive"] != DBNull.Value && (bool)src["IsActive"]))
                .ForMember(dest => dest.IssueReason, opt => opt.MapFrom(src =>
                    src["IssueReason"] != DBNull.Value ? (byte?)src["IssueReason"] : null))
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src =>
                    src["CreatedByUserId"] != DBNull.Value ? (int?)src["CreatedByUserId"] : null));
        }
    }
}
