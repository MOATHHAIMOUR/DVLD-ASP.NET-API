using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface ILocalDrivingApplicationRepository : IGenericRepository<LocalDrivingLicenseApplication>
    {
        public Task<(int ApplicationId, int RenewLicenseId)> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId, DateTime expirationDate);

        public Task<(int ApplicationId, int ReplacementForLostLicenseId)> ReplaceForLostLocalDrivingLicenseAsync(int licenseId,
        int createdByUserId,
        DateTime expirationDate);

        public Task<(int ApplicationId, int ReplacementDamageForLicenseId)> ReplaceForDamageLocalDrivingLicenseAsync(
        int licenseId,
        int createdByUserId,
        DateTime expirationDate);

        public Task<int> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense);

        public Task<int> ReleaseDetainLocalDrivingLicenseAsync(
         int licenseId,
         DateTime detainDate,
         decimal fineFees,
         int releasedByUserId);

        public Task<bool> IsLicenseDetainedAsync(int licenseId);

        public Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId);
    }
}
