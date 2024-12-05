using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.PersonDtos;
using DVLD.Domain.Entites;
using DVLD.Domain.views.Person;

namespace DVLD.Application.Services.IServices
{
    public interface IPersonServices
    {
        public Task<Result<IEnumerable<PeopleView>>> GetAllPeopleViewAsync(PeopleSearchParamsDTO peopleSearchParamsDTO);

        public Task<Result<Person>> GetPersonAsync(int? personId, string? NationalNo);

        public Task<Result<string>> DeletePersonByIdAsync(int PersonId);

        public Task<Result<int>> AddPersonAsync(Person person);

        public Task<Result<string>> UpdatePersonAsync(Person person);
    }
}
