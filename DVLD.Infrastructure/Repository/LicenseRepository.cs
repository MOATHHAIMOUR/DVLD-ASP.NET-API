using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;

namespace DVLD.Infrastructure.Repository
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly IMapper _mapper;

        public LicenseRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<int> AddAsync(string storedProcedure, License entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<License>> GetAllAsync(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Task<License?> GetAsync(string storedProcedure, string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(string storedProcedure, string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string storedProcedure, License entity)
        {
            throw new NotImplementedException();
        }
    }
}
