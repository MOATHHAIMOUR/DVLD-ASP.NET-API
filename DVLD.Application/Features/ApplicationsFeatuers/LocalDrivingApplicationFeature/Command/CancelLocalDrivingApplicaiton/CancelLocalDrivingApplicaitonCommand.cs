using DVLD.Application.Common.ApiResponse;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.CancelLocalDrivingApplicaiton
{
    public class CancelLocalDrivingApplicaitonCommand : IRequest<ApiResponse<string>>
    {
        public int localDrivingApplicationId { get; set; }

        public CancelLocalDrivingApplicaitonCommand(int localDrivingApplicationId)
        {
            this.localDrivingApplicationId = localDrivingApplicationId;
        }
    }
}
