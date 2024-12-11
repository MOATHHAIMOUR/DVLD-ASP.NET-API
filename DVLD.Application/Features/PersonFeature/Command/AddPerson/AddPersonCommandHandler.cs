using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DVLD.Application.Features.PersonFeature.Command.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, ApiResponse<string>>
    {
        private readonly IPersonServices _personServices;
        private readonly IMapper _mapper;
        public AddPersonCommandHandler(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            IFormFile PersonImage = request.AddPersonDTO.ImageFile;

            Person person = _mapper.Map<Person>(request.AddPersonDTO);

            var result = await _personServices.AddPersonAsync(person, PersonImage);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<string>([result.Error.Message]);

            return ApiResponseHandler.Success<string>(
                   message: $"{request.AddPersonDTO.FirstName} has been added successfully with ID {result.Value}"
               );
        }
    }
}
