using DVLD.Application.Services.IServices;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Application.Services
{
    public class LicnsesServices : ILicnsesServices
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ITestRepository _testRepository;
        private IDBViewsRepository _dBViewsRepository;

        public LicnsesServices(ILicenseRepository licenseRepository, ITestRepository testRepository, IDBViewsRepository dBViewsRepository)
        {
            _licenseRepository = licenseRepository;
            _testRepository = testRepository;
            _dBViewsRepository = dBViewsRepository;
        }


    }
}
