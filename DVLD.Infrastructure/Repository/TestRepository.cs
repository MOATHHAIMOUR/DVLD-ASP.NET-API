using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;

namespace DVLD.Infrastructure.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly IMapper _mapper;

        public TestRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<int> AddAsync(string storedProcedure, Test entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Test>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<Test?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, Test entity)
        {
            throw new NotImplementedException();
        }
    }
}
