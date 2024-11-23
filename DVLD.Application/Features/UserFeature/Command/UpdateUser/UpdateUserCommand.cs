
                        using DVLD.Application.Common.ApiResponse;
                        using DVLD.Application.DTO.Users;
                        using MediatR;

                        namespace DVLD.Application.Features.UserFeature.Command.UpdateUser
                        {
                            public class UpdateUserCommand : IRequest<ApiResponse<string>>
                            {
                                public UpdateUserDTO UpdateUserDTO { get; set; }
                                public UpdateUserCommand(UpdateUserDTO updateUserDTO)
                                {
                                    UpdateUserDTO = updateUserDTO;
                                }
                            }
                        }
                        