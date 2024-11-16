using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Command.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<ApiResponse<string>>
    {
        public UpdatePersonDTO UpdatePersonDTO { get; set; }

        public UpdatePersonCommand(UpdatePersonDTO updatePersonDTO)
        {
            this.UpdatePersonDTO = updatePersonDTO;
        }
    }
}
