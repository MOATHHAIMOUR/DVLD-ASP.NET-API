using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views;

namespace DVLD.Domain.IRepository
{
    public interface ISharedRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
    }
}
