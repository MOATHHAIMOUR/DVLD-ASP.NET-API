using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;

namespace DVLD.Application.Services
{
    public class SharedServices : ISharedServices
    {
        private readonly ISharedRepository _sharedRepository;

        public SharedServices(ISharedRepository sharedRepository)
        {
            _sharedRepository = sharedRepository;
        }

        public async Task<Result<List<Country>>> GetAllCountriesAsync()
        {
            var countries = await _sharedRepository.GetAllCountriesAsync();

            return Result<List<Country>>.Success(countries);
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            return await _sharedRepository.GetCountryByIdAsync(countryId);
        }

        public async Task<List<ApplicationType>> GetAllApplicationTypesAsync()
        {
            return await _sharedRepository.GetAllApplicationTypesAsync();
        }

        public async Task<Result<string>> UpdateApplicationType(ApplicationType applicationType)
        {
            var result = await _sharedRepository.UpdateApplicationType(applicationType);

            return result ?
              Result<string>.Success($"ApplicationType with id: {applicationType.ApplicationTypeId} is Updated successfully")
              :
              Result<string>.Failure(Error.RecoredNotFound($"ApplicationType was not found"));
        }

        public async Task<Result<int>> GetRowCountAsync(string tableName)
        {
            return Result<int>.Success(await _sharedRepository.GetRowCountAsync(tableName));
        }
    }
}
