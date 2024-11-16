using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, ApiResponse<PersonDTO>>
    {
        private readonly IPersonServices _personServices;
        private readonly IMapper _mapper;

        public GetPersonQueryHandler(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PersonDTO>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            
            var result = await _personServices.GetPersonAsync(request.PersonId, request.NationalNo);



            return  result.IsSuccess ?
                ApiResponseHandler.Success<PersonDTO>(_mapper.Map<PersonDTO>(result.Value)):
                ApiResponseHandler.NotFound<PersonDTO>(result.Error.Message);
        }
    }
}
