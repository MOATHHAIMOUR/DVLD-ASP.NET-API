using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Commands.AddNewInternationalLicense
{
    public class AddNewInternationalLicenseCommandHandler : IRequestHandler<AddNewInternationalLicenseCommand, ApiResponse<InternationalLicenseResultDTO>>
    {
        private readonly IInternationalLicenseServices _internationalLicenseServices;
        private readonly IMapper _mapper;

        public AddNewInternationalLicenseCommandHandler(IInternationalLicenseServices internationalLicenseServices, IMapper mapper)
        {
            _internationalLicenseServices = internationalLicenseServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<InternationalLicenseResultDTO>> Handle(AddNewInternationalLicenseCommand request, CancellationToken cancellationToken)
        {
            InternationalLicense internationalLicense = _mapper.Map<InternationalLicense>(request.AddNewInternationalLicenseDTO);

            var result = await _internationalLicenseServices.AddNewInternationalLicenseAsync(internationalLicense);

            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<InternationalLicenseResultDTO>([result.Error.Message]);

            // Assuming result contains ApplicationId and InternationalLicenseId as a tuple
            return ApiResponseHandler.Success(new InternationalLicenseResultDTO
            {
                ApplicationId = result.Value.ApplicationId,
                InternationalLicenseId = result.Value.InternationalLicenseId
            });
        }
    }
}
