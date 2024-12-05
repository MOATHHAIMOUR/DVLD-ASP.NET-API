using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Commandes.AddTestAppointment
{
    public class AddTestAppointmentCommandHandler : IRequestHandler<AddTestAppointmentCommand, ApiResponse<string>>
    {
        private IMapper _mapper;
        private ITestServices _testServices;

        public AddTestAppointmentCommandHandler(IMapper mapper, ITestServices testServices)
        {
            _mapper = mapper;
            _testServices = testServices;
        }

        public async Task<ApiResponse<string>> Handle(AddTestAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Map the incoming DTO to the TestAppointment entity
            var testAppointment = _mapper.Map<TestAppointment>(request.appintmentDTO);

            // Call the service to add the test appointment
            var result = await _testServices.AddTestAppointmentAsync(testAppointment);

            // Handle the result and construct the appropriate API response
            if (!result.IsSuccess)
            {
                return ApiResponseHandler.BadRequest<string>([result.Error?.Message]);
            }

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
