using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.views.Person;
using Microsoft.AspNetCore.Http;

namespace DVLD.Application.Services.IServices
{
    public interface IPersonServices
    {
        public Task<Result<IEnumerable<PersonView>>> GetAllPeopleViewAsync(PeopleSearchParameters PeopleSearchParameters);

        public Task<Result<Person>> GetPersonAsync(int? personId, string? NationalNo);

        public Task<Result<string>> DeletePersonByIdAsync(int PersonId);

        public Task<Result<int>> AddPersonAsync(Person person, IFormFile personImage);

        public Task<Result<string>> UpdatePersonAsync(Person person, string imagePath, IFormFile newProfileImage);
    }
}
