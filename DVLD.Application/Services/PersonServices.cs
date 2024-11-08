using DVLD.Application.Common.ResultPattern;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.IRepository;
using Microsoft.AspNetCore.Http;

namespace DVLD.Application.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;

        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result<List<Person>>> GetAllPersonsAsync()
        {
            return Result<List<Person>>.Success(await _personRepository.GetAllAsync());
        }
    }
}
