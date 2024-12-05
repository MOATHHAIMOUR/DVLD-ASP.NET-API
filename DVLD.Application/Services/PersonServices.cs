using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.DTO.PersonDtos;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.Person;

namespace DVLD.Application.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;
        private readonly ISharedServices _sharedServices;

        public PersonServices(IPersonRepository personRepository, ISharedServices sharedServices)
        {
            _personRepository = personRepository;
            _sharedServices = sharedServices;
        }

        public async Task<Result<IEnumerable<PeopleView>>> GetAllPeopleViewAsync(PeopleSearchParamsDTO peopleSearchParamsDTO)
        {
            return Result<IEnumerable<PeopleView>>.Success(await _personRepository.GetAllAsync<PeopleView>(PersonStoredProcedures.SP_GetPeople, peopleSearchParamsDTO));
        }

        public async Task<Result<Person>> GetPersonAsync(int? personId, string? NationalNo)
        {
            var person = await _personRepository.GetPersonByIdOrNationalNo(personId, NationalNo);

            if (person != null)
            {
                person.Country = await _sharedServices.GetCountryByIdAsync(person.CountryId);
            }

            return person == null
                ? Result<Person>.Failure(Error.RecoredNotFound("Person not found"))
                : Result<Person>.Success(person);
        }

        public async Task<Result<int>> AddPersonAsync(Person person)
        {
            int insertedId = await _personRepository.AddAsync(PersonStoredProcedures.SP_AddPerson, person);

            return Result<int>.Success(insertedId);
        }

        public async Task<Result<string>> DeletePersonByIdAsync(int PersonId)
        {
            bool result = await _personRepository.DeleteAsync(PersonStoredProcedures.SP_DeletePersonById, "PersonId", PersonId);

            return result ?
                Result<string>.Success($"User with ID {PersonId} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID {PersonId}."));
        }

        public async Task<Result<string>> UpdatePersonAsync(Person person)
        {
            bool result = await _personRepository.UpdateAsync(PersonStoredProcedures.SP_UpdatePerson, person);

            return result ?
                Result<string>.Success($"Person with name {person.FirstName + " " + person.LastName} Updated scsessfully")
                :
                 Result<string>.Failure(Error.RecoredNotFound($"person with this id: {person.PersonId} is not exist"));

        }


    }
}
