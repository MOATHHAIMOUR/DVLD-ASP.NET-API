using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository.Base;

namespace DVLD.Infrastructure.Repository
{
    public class LicenseRepository : GenericRepository<License>, ILicenseRepository
    {
        public LicenseRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
