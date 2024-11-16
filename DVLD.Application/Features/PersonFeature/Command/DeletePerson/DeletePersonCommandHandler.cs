using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Command.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, ApiResponse<string>>
    {
        private readonly IPersonServices _personServices;

        public DeletePersonCommandHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        public async Task<ApiResponse<string>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
           var result =  await _personServices.DeletePersonByIdAsync(request.PersonId);

            if (result.IsSuccess)
            {
                return ApiResponseHandler.Deleted<string>(message:result.Value);
            }
            else
            {
                return ApiResponseHandler.NotFound<string>(message: result.Error.Message);
            }

        }
    }
}
