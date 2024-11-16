using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Command.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, ApiResponse<string>>
    {
        private readonly IPersonServices _personServices;
        private readonly  IMapper _mapper;

        public UpdatePersonCommandHandler(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            Person person = _mapper.Map<Person>(request.UpdatePersonDTO);
            var result = await _personServices.UpdatePersonAsync(person);

            return result.IsSuccess ?
                 ApiResponseHandler.Success(result.Value!)
                 :
                 ApiResponseHandler.NotFound<string>(result.Error.Message);
        }
    }
}
