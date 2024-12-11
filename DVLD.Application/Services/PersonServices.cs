using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;
using DVLD.Domain.StoredProcdure;
using DVLD.Domain.views.Person;
using Microsoft.AspNetCore.Http;

namespace DVLD.Application.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;
        private readonly ISharedServices _sharedServices;
        private readonly IDBViewsRepository _dBViewsRepository;
        private readonly IImageServices _imageServices;

        public PersonServices(IPersonRepository personRepository, ISharedServices sharedServices, IDBViewsRepository dBViewsRepository, IImageServices imageServices)
        {
            _personRepository = personRepository;
            _sharedServices = sharedServices;
            _dBViewsRepository = dBViewsRepository;
            _imageServices = imageServices;
        }

        public async Task<Result<IEnumerable<PersonView>>> GetAllPeopleViewAsync(PeopleSearchParameters peopleSearchParamsDTO)
        {
            return Result<IEnumerable<PersonView>>.Success(await _dBViewsRepository.GetPeopleViewAync(PersonStoredProcedures.SP_GetPeople, peopleSearchParamsDTO));
        }

        public async Task<Result<Person>> GetPersonAsync(int? personId, string? NationalNo)
        {
            var person = await _personRepository.GetPersonByIdOrNationalNo(personId, NationalNo);

            if (person != null)
            {
                person.Country = await _sharedServices.GetCountryByIdAsync(person.CountryId);
            }

            return person == null
                ? Result<Person>.Failure(Error.RecoredNotFound("Person is not found"))
                : Result<Person>.Success(person);
        }

        public async Task<Result<int>> AddPersonAsync(Person person, IFormFile personImage)
        {
            // cheek ifer person with the national-no is exist 
            if (await _personRepository.IsExist(PersonStoredProcedures.SP_CheckIsPersonExist, "NationalNo", person.NationalNo))
                return Result<int>.Failure(Error.ValidationError("The provided National Number already exists."));


            string? imagePath = null;
            int insertedPersonId = -1;

            // if user provide image save it on cloudeinaery
            if (personImage != null)
            {
                imagePath = await _imageServices.UploadImageAsync(personImage);

                if (imagePath == null)
                    return Result<int>.Failure(Error.ValidationError("Faild to upload image please try again later"));
                else person.ImagePath = imagePath;
            }

            insertedPersonId = await _personRepository.AddAsync(PersonStoredProcedures.SP_AddPerson, person);


            if (insertedPersonId == -1)
                return Result<int>.Failure(Error.ValidationError("There was an error please try again later"));

            return Result<int>.Success(insertedPersonId);
        }

        public async Task<Result<string>> DeletePersonByIdAsync(int PersonId)
        {
            bool result = await _personRepository.DeleteAsync(PersonStoredProcedures.SP_DeletePersonById, "PersonId", PersonId);

            return result ?
                Result<string>.Success($"User with ID {PersonId} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID {PersonId}."));
        }

        public async Task<Result<string>> UpdatePersonAsync(Person person, string CloudienryImagePath, IFormFile newProfileImage)
        {

            //  get perosn image if has
            string? currentImagePath = (await _personRepository.GetPersonByIdOrNationalNo(person.PersonId, null))?.ImagePath;


            //  delete old image   
            if (newProfileImage != null)
            {
                // delete old image
                if (currentImagePath != null && !await _imageServices.DeleteImageAsync(currentImagePath))
                    Result<string>.Failure(Error.ValidationError($"Failed to update person info please try again"));

                // upload new one
                string? newUplodedImage = await _imageServices.UploadImageAsync(newProfileImage);
                if (newUplodedImage == null)
                    Result<string>.Failure(Error.ValidationError($"Failed to update person info please try again"));
                else
                    person.ImagePath = newUplodedImage;

            }
            else
            {
                // user delete his profile image
                if (currentImagePath != null && currentImagePath != CloudienryImagePath && !await _imageServices.DeleteImageAsync(currentImagePath))
                    return Result<string>.Failure(Error.ValidationError($"Failed to update person info please try again"));
            }


            // update person info

            bool result = await _personRepository.UpdateAsync(PersonStoredProcedures.SP_UpdatePerson, person);

            return result ?
                Result<string>.Success($"Person with name {person.FirstName + " " + person.LastName} Updated scsessfully")
                :
                 Result<string>.Failure(Error.RecoredNotFound($"person with this id: {person.PersonId} is not exist"));

        }

    }
}
