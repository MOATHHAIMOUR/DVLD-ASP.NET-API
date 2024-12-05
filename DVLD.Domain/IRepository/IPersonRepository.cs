using DVLD.Domain.Entites;
using DVLD.Domain.IRepository.Base;

namespace DVLD.Domain.IRepository
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<Person?> GetPersonByIdOrNationalNo(int? personId, string? nationalNo);
    }
}
