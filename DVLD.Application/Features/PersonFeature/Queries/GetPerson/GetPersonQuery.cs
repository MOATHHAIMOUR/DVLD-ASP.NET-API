using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<ApiResponse<PersonDTO>>
    {
        public GetPersonQuery(int? personId, string? nationalNo)
        {
            PersonId = personId;
            NationalNo = nationalNo;
        }

        public int ? PersonId { get; set; } 

        public string? NationalNo {  get; set; }   

    }
}
