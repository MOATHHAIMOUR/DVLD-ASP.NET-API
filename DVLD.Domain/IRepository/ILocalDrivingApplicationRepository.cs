using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface ILocalDrivingApplicationRepository : IGenericRepository<LocalDrivingLicenseApplication>
    {
        public Task<(int ApplicationId, int RenewLicenseId)> RenewLocalDrivingLicenseAsync(int licenseId, int createdByUserId);

        public Task<(int ApplicationId, int ReplacementForLostLicenseId)> ReplaceForLostLocalDrivingLicenseAsync(int licenseId,
        int createdByUserId,
        DateTime expirationDate);


        public Task<bool> IsApplicantHasLocalLicenseAsync(int licenseClassId, int applicationId);

        public Task<(int ApplicationId, int ReplacementDamageForLicenseId)> ReplaceForDamageLocalDrivingLicenseAsync(
        int licenseId,
        int createdByUserId,
        DateTime expirationDate);

        public Task<bool> IsApplicantHasAcActiveLocalDrivingApplication(int personId, int LicenseClassId);

        public Task<bool> CancelLocalDrivingApplication(int localDrivingApplication);

        public Task<bool> IsLocalDrivingApplicationCompletedAsync(int localDrivingApplication);

        public Task<bool> IsLocalDrivingApplicationCompeletedOrCancelled(int LocalDrivingApplicationId);

        public Task<bool> IsLicenseDetainedAsync(int licenseId);

        public Task<bool> IsLocalDrivingLicenseExistsAsync(int licenseId);



        public Task<bool> IsApplicantHasAcActiveApplicationPerApplicationType(int personId, int applicationTypeId);
        public Task<bool> IsApplicanHasAlreadyActiveLicenseWithSameType(int personId, int applicationTypeId);

    }
}
