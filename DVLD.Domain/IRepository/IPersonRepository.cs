using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views;

namespace DVLD.Domain.IRepository
{
    public interface IPersonRepository
    {
        Task<List<PeopleView>> GetPeopleViewAsync(
            int? PersonId = null,
            string? NationalNo = null,
            string? FirstName = null,
            string? SecondName = null,
            string? ThirdName = null,
            string? LastName = null,
            EnumGender? Gender = null,
            string? Phone = null,
            string? Email = null,
            string? CountryName = null,
            int PageNumber = 1,
            int pageSize =5,
            string? SortBy = null,
            EnumSortDirection sortDirection = EnumSortDirection.ASC);

        Task<Person?> GetPersonByIdOrNationalNo(int? personId, string? nationalNo);

        Task<int> AddPersonAsycn(Person person);
        Task<bool> UpdatePersonAsycn(Person person);

        
        Task<bool> DeletePersonAsync(int personId);
    }
}
