using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository.Base;

namespace DVLD.Infrastructure.Repository
{
    public class DetainedLicenseRepository : GenericRepository<DetainedLicense>, IDetainedLicenseRepository
    {
        public DetainedLicenseRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
