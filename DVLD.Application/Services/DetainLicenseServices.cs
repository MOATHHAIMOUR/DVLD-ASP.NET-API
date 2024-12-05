using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;

namespace DVLD.Application.Services
{
    public class DetainLicenseServices : IDetainLicenseServices
    {
        private readonly IDetainedLicenseRepository _detainedLicenseRepository;


        public DetainLicenseServices(IDetainedLicenseRepository detainedLicenseRepository)
        {
            _detainedLicenseRepository = detainedLicenseRepository;
        }

        public Task<Result<int>> DetainLocalDrivingLicenseAsync(DetainedLicense detainedLicense)
        {

        }
    }
}
