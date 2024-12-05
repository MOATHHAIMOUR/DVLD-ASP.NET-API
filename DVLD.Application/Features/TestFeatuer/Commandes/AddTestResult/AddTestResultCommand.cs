using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.TestDTOs;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Commandes.AddTestResult
{
    public class AddTestResultCommand : IRequest<ApiResponse<string>>
    {
        public AddTestResultCommand(AddTestDTO addTestDTO)
        {
            AddTestDTO = addTestDTO;
        }

        public AddTestDTO AddTestDTO { set; get; }
    }
}
