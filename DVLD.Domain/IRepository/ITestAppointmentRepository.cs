using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface ITestAppointmentRepository : IGenericRepository<TestAppointment>
    {
        public Task<bool> IsApplicantPassTestAsync(int localDrivingApplicationId, int testTypeId);

        public Task<bool> IsApplicantHasAppointmentTestNotLockedAsync(int localDrivingApplicationId, int testTypeId);

    }
}