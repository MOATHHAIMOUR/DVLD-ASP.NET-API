using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views;

namespace DVLD.Application.Services.IServices
{
    public interface ISharedServices
    {
        public Task<Result<List<Country>>> GetAllCountriesAsync();

    }
}
