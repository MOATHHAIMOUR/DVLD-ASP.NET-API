using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views;

namespace DVLD.Application.Services.IServices
{
    public interface IPersonServices
    {

        public Task<Result<List<PeopleView>>> GetAllPeopleViewAsync(
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
            string? SortBy = null,
            int PageNumber = 1,
            int PageSize = 10,
            EnumSortDirection sortDirection = EnumSortDirection.ASC );

        public Task<Result<Person>> GetPersonAsync(int? personId, string? NationalNo);

        public Task<Result<string>> DeletePersonByIdAsync(int PersonID);

        public Task<Result<int>> AddPersonAsync(Person person);

        public Task<Result<string>> UpdatePersonAsync(Person person);

    }
}
