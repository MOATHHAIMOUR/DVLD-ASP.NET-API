using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface ILicenseRepository : IGenericRepository<License>
    {

        public Task<(int ApplicationId, int ReplacementDamageForLicenseId)> ReplaceDamageLicenseAsync(string storedProcedure, int LicenseId, int userId);
        public Task<(int ApplicationId, int ReplacementLostLocalDrivingLicense)> ReplaceLostLicenseAsync(string storedProcedure, int LicenseId, int userId);

    }
}
