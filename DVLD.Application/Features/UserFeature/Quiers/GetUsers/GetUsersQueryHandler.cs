using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.Users;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using MediatR;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<List<GetUserDTO>>>
    {
        private readonly IUserServices _userServices;

        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetUserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDto = _mapper.Map<List<User>, List<GetUserDTO>>(await _userServices.GetUsers(

                request.UserSearchParamsDTO.UserId,
                request.UserSearchParamsDTO.PersonId,
                request.UserSearchParamsDTO.UserName,
                request.UserSearchParamsDTO.IsActive,
                request.UserSearchParamsDTO.SortBy,
                request.UserSearchParamsDTO.SortDireaction == "ASC" ? EnumSortDirection.ASC : EnumSortDirection.DESC,
                request.UserSearchParamsDTO.PageSize,
                request.UserSearchParamsDTO.PageNumber
                ));

            return ApiResponseHandler.Success(usersDto);

        }
    }
}
