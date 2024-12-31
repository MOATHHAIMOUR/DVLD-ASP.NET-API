using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.License.InternationalLicense;
using DVLD.Domain.views.License.LocalLicense;

namespace DVLD.Application.Services
{
    public class InternationalLicenseServices : IInternationalLicenseServices
    {
        private readonly IInternationalLicenseRepository _internationalLicenseRepository;
        private readonly ILicenseRepository _licenseRepository;
        private readonly IDBViewsRepository _dBViewsRepository;

        public InternationalLicenseServices(IInternationalLicenseRepository internationalLicenseRepository, ILicenseRepository licenseRepository, IDBViewsRepository dBViewsRepository)
        {
            _internationalLicenseRepository = internationalLicenseRepository;
            _licenseRepository = licenseRepository;
            _dBViewsRepository = dBViewsRepository;
        }

        public async Task<Result<(int ApplicationId, int InternationalLicenseId)>> AddNewInternationalLicenseAsync(InternationalLicense internationalLicense)
        {
            // Check if the provided license is an ordinery 
            LicenseDetailsView licenseDetailsView = (await _dBViewsRepository.GetLicenseInfo(null, internationalLicense.IssuedUsingLocalLicenseId, null))!;

            if (licenseDetailsView.LicenseClassId != Domain.Enums.EnumLicenseClass.OrdinaryDrivingLicense)
                return Result<(int ApplicationId, int InternationalLicenseId)>.Failure(Error.ValidationError("Local License must be of class Ordinery"));

            // Check if the driver already has an international license
            bool hasInternationalLicense = await _internationalLicenseRepository.HasInternationalLicenseAsync(licenseDetailsView.DriverId);

            if (hasInternationalLicense)
                return Result<(int ApplicationId, int InternationalLicenseId)>.Failure(Error.ValidationError("Applicant already has an international license"));

            // Validate the local driving license
            if (licenseDetailsView.IsDetain)
                return Result<(int ApplicationId, int InternationalLicenseId)>.Failure(Error.ValidationError("Applicant's Ordinary Local License is detained"));

            if (!licenseDetailsView.IsActive)
                return Result<(int ApplicationId, int InternationalLicenseId)>.Failure(Error.ValidationError("Applicant's Ordinary Local License is not active"));

            if (licenseDetailsView.ExpirationDate < DateTime.UtcNow)
                return Result<(int ApplicationId, int InternationalLicenseId)>.Failure(Error.ValidationError("Applicant's Ordinary Local License is expired"));


            // Add the international license and get the IDs
            var (applicationId, internationalLicenseId) = await _internationalLicenseRepository.AddInternationalLicenseAsync(
                InternationalLicenseStoredProcedures.SP_AddNewInternationalLicense,
                internationalLicense
            );

            // Return success with the tuple
            return Result<(int ApplicationId, int InternationalLicenseId)>.Success((applicationId, internationalLicenseId));
        }

        public async Task<Result<IEnumerable<InternationalLicense>>> GetAllInternationalLicenses()
        {
            var result = await _internationalLicenseRepository.GetAllAsync("SP_GetAllInternationalLicenses");


            return Result<IEnumerable<InternationalLicense>>.Success(result);
        }

        public async Task<Result<InternationalLicenseView>> GetInternationalLicenseViewAsync(int InternationalLicenseId)
        {
            InternationalLicenseView? internationalLicense = await _dBViewsRepository.GetInternationalLicenseViewAsync(InternationalLicenseId);

            if (internationalLicense == null)
                return Result<InternationalLicenseView>.Failure(Error.RecoredNotFound($"International license with Id: {InternationalLicenseId} is not found"));

            return Result<InternationalLicenseView>.Success(internationalLicense);
        }
    }
}
