using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.TestDTOs;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Commandes.AddTestAppointment
{
    public class AddTestAppointmentCommand : IRequest<ApiResponse<string>>
    {
        public AddTestAppointmentCommand(AddTestAppintmentDTO appintmentDTO)
        {
            this.appintmentDTO = appintmentDTO;
        }

        public AddTestAppintmentDTO appintmentDTO
        { set; get; }

    }
}
