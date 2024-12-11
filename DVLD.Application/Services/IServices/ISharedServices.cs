using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;

namespace DVLD.Application.Services.IServices
{
    public interface ISharedServices
    {
        public Task<Result<List<Country>>> GetAllCountriesAsync();

        public Task<Country> GetCountryByIdAsync(int countryId);

        public Task<List<ApplicationType>> GetAllApplicationTypesAsync();
        public Task<Result<int>> GetRowCountAsync(string tableName);
        public Task<Result<string>> UpdateApplicationType(ApplicationType applicationType);

    }
}
