using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface ITestRepository : IGenericRepository<Test>
    {

        public Task<bool> IsApplicantPassAllTests(string storedProcedure, int applicationId);


    }
}
