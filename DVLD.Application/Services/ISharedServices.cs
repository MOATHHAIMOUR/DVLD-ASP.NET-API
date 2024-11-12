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
    }
}
