using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Commandes.AddTestResult
{
    public class AddTestResultCommandHandler : IRequestHandler<AddTestResultCommand, ApiResponse<string>>
    {
        private readonly ITestServices _testServices;
        private readonly IMapper _mapper;

        public AddTestResultCommandHandler(ITestServices testServices, IMapper mapper)
        {
            _testServices = testServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<Test>(request.AddTestDTO);
            var result = await _testServices.AddTestResult(test);

            return ApiResponseHandler.Success<string>(data: result.Value!);
        }
    }
}
