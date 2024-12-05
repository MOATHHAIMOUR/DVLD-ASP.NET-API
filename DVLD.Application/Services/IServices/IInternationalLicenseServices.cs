using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;
using DVLD.Domain.views.License.InternationalLicense;

namespace DVLD.Application.Services.IServices
{
    public interface IInternationalLicenseServices
    {
        public Task<Result<(int ApplicationId, int InternationalLicenseId)>> AddNewInternationalLicenseAsync(InternationalLicense internationalLicense);

        public Task<Result<InternationalLicenseView>> GetInternationalLicenseViewAsync(int InternationalLicenseId);

        public Task<Result<IEnumerable<InternationalLicense>>> GetAllInternationalLicenses();


    }
}
