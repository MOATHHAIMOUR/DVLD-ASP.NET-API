using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.PersonDtos;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Command.AddPerson
{
    public class AddPersonCommand : IRequest<ApiResponse<string>>
    {
        public AddPersonDTO AddPersonDTO { get; set; }
        public AddPersonCommand(AddPersonDTO addPersonDTO)
        {
            AddPersonDTO = addPersonDTO;
        }

    }
}
