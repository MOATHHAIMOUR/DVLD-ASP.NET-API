using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.SharedDTOs;
using MediatR;

namespace DVLD.Application.Features.SettingsFeatuer.Command.UpdateApplicationType
{
    public class UpdateApplicationTypeCommand : IRequest<ApiResponse<string>>
    {
        public UpdateApplicationTypeDTO UpdateApplicationTypeDTO;
        public UpdateApplicationTypeCommand(UpdateApplicationTypeDTO updateApplicationTypeDTO)
        {
            UpdateApplicationTypeDTO = updateApplicationTypeDTO;
        }
    }
}
