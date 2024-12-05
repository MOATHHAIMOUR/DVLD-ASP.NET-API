using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface IInternationalLicenseRepository : IGenericRepository<InternationalLicense>
    {
        public Task<bool> HasInternationalLicenseAsync(int driverId);

        public Task<(int applicationId, int internationalLicenseId)> AddInternationalLicenseAsync(string storedProcedure, InternationalLicense entity);


        public Task<(bool IsValid, int LicenseId)> CheckDriverHasOrdinaryLocalDrivingLicenseAsync(int driverId);
    }
}
