using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.PersonDtos;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, ApiResponse<GetPersonDTO>>
    {
        private readonly IPersonServices _personServices;
        private readonly IMapper _mapper;

        public GetPersonQueryHandler(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetPersonDTO>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {

            var result = await _personServices.GetPersonAsync(request.PersonId, request.NationalNo);



            return result.IsSuccess ?
                ApiResponseHandler.Success<GetPersonDTO>(_mapper.Map<GetPersonDTO>(result.Value)) :
                ApiResponseHandler.NotFound<GetPersonDTO>(result.Error.Message);
        }
    }
}
