using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Quiers.GetAllInternationalLicense
{
    internal class GetAllInternationalLicenseQueryHandler : IRequestHandler<GetAllInternationalLicenseQuery, ApiResponse<IEnumerable<GetInternationalLicenseDTO>>>
    {
        private readonly IInternationalLicenseServices _internationalLicenseServices;
        private readonly IMapper _mapper;

        public GetAllInternationalLicenseQueryHandler(IInternationalLicenseServices internationalLicenseServices, IMapper mapper)
        {
            _internationalLicenseServices = internationalLicenseServices;
            _mapper = mapper;
        }


        public async Task<ApiResponse<IEnumerable<GetInternationalLicenseDTO>>> Handle(GetAllInternationalLicenseQuery request, CancellationToken cancellationToken)
        {
            var result = await _internationalLicenseServices.GetAllInternationalLicenses();

            if (!result.IsSuccess)
                return ApiResponseHandler.NotFound<IEnumerable<GetInternationalLicenseDTO>>([result.Error.Message]);

            var dtos = _mapper.Map<IEnumerable<GetInternationalLicenseDTO>>(result.Value);

            return ApiResponseHandler.Success(dtos);
        }
    }
}
