using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;
using DVLD.Domain.views;

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

        public async Task<Result<List<PeopleView>>> GetAllPeopleViewAsync(int? PersonId = null, string? NationalNo = null, string? FirstName = null, string? SecondName = null, string? ThirdName = null, string? LastName = null, EnumGender? Gender = null, string? Phone = null, string? Email = null, string? CountryName = null, string? SortBy = null, int PageNumber = 1, int PageSize = 10, EnumSortDirection sortDirection = EnumSortDirection.ASC)
        {
            return Result<List<PeopleView>>.Success(await _personRepository.GetPeopleViewAsync(
                                                PersonId,
                                                NationalNo,
                                                FirstName,
                                                SecondName,
                                                ThirdName,
                                                LastName,
                                                Gender,
                                                Phone,
                                                Email,
                                                CountryName,
                                                PageNumber,
                                                PageSize,
                                                SortBy,
                                                sortDirection
                                            ));
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
            int insertedId = await _personRepository.AddPersonAsycn(person);

            return Result<int>.Success(insertedId);
        }

        public async Task<Result<string>> DeletePersonByIdAsync(int PersonID)
        {
            bool result = await _personRepository.DeletePersonAsync(PersonID);

            return result ?
                Result<string>.Success($"User with ID {PersonID} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID {PersonID}."));
        }

        public async Task<Result<string>> UpdatePersonAsync(Person person)
        {
            bool result = await _personRepository.UpdatePersonAsycn(person);

            return result ?
                Result<string>.Success($"Person with name {person.FirstName + " " + person.LastName} Updated scsessfully")
                :
                 Result<string>.Failure(Error.RecoredNotFound($"person with this id: {person.PersonId} is not exist"));

        }


    }
}
