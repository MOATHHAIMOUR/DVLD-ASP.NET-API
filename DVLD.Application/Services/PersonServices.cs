using DVLD.Application.Common.Errors;
using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Enums;
using DVLD.Domain.IRepository;
using DVLD.Domain.views;

namespace DVLD.Application.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;

        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result<string>> DeletePersonById(int PersonID)
        {
           bool result = await _personRepository.DeleteAsync(PersonID);

            return result ?
                Result<string>.Success($"User with ID {PersonID} has been successfully deleted.")
                :
                Result<string>.Failure(Error.RecoredNotFound($"Deletion failed: No user found with ID {PersonID}."));
        }

        public async Task<Result<List<PeopleView>>> GetAllPeopleViewAsync(int? PersonId = null, string? NationalNo = null, string? FirstName = null, string? SecondName = null, string? ThirdName = null, string? LastName = null, EnumGender? Gender = null, string? Phone = null, string? Email = null, string? CountryName = null, int? PageNumber = null, int? PageSize = null)
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
                          CountryName
                      ));
        }

       
    }
}
