using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.TestDTOs;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentViewQuery
{
    public class GetTestAppointmentViewQuery : IRequest<ApiResponse<IEnumerable<TestAppointmentView>>>
    {
        public SearchTestAppointmentViewDTO SearchTestAppointmentViewDTO { get; set; }
        public GetTestAppointmentViewQuery(SearchTestAppointmentViewDTO searchTestAppointmentViewDTO)
        {
            SearchTestAppointmentViewDTO = searchTestAppointmentViewDTO;
        }

    }
}
