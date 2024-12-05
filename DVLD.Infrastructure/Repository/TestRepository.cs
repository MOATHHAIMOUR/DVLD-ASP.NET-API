using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository.Base;

namespace DVLD.Infrastructure.Repository
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(IMapper mapper) : base(mapper)
        {
        }


    }
}
