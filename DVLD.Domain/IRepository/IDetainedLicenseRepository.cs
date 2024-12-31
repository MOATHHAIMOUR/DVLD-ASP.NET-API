using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface IDetainedLicenseRepository : IGenericRepository<DetainedLicense>
    {
        public Task<int> ReleaseDetainLocalDrivingLicenseAsync(
        int licenseId,
        DateTime detainDate,
        int releasedByUserId);


        public Task<bool> IsLicenseDetain(int licenseId);



    }
}
