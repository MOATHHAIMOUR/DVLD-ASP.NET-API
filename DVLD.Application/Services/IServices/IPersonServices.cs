using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites;

namespace DVLD.Application.Services.IServices
{
    public interface IPersonServices
    {

        public Task<Result<List<Person>>> GetAllPersonsAsync();



    }
}
