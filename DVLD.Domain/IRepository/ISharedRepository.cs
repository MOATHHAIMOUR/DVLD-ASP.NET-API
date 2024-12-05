using DVLD.Domain.Entites;

namespace DVLD.Domain.IRepository
{
    public interface ISharedRepository
    {
        Task<List<Country>> GetAllCountriesAsync();

        public Task<List<ApplicationType>> GetAllApplicationTypesAsync();

        public Task<Country> GetCountryByIdAsync(int countryId);

        public Task<bool> UpdateApplicationType(ApplicationType applicationType);

    }
}
