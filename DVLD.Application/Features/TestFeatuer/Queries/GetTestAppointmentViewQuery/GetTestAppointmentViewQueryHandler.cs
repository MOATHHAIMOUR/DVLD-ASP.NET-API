using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentViewQuery
{
    public class GetTestAppointmentViewQueryHandler : IRequestHandler<GetTestAppointmentViewQuery, ApiResponse<IEnumerable<TestAppointmentView>>>
    {
        private readonly ITestServices _testServices;


        public GetTestAppointmentViewQueryHandler(ITestServices testServices)
        {
            _testServices = testServices;
        }

        public async Task<ApiResponse<IEnumerable<TestAppointmentView>>> Handle(GetTestAppointmentViewQuery request, CancellationToken cancellationToken)
        {
            var result = await _testServices.GetTestAppointmentViewAsync(request.SearchTestAppointmentViewDTO);


            int totalPages = (int)Math.Ceiling(result.Value?.Count() ?? 0 / Convert.ToDecimal(request.SearchTestAppointmentViewDTO.PageSize));

            return ApiResponseHandler.Success(result.Value ?? [], new
            {
                totalPages
            });



        }


    }
}
