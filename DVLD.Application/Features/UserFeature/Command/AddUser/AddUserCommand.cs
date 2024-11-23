
                        using DVLD.Application.Common.ApiResponse;
                        using DVLD.Application.DTO.Users;
                        using MediatR;

                        namespace DVLD.Application.Features.UserFeature.Command.AddUser
                        {
                            public class AddUserCommand : IRequest<ApiResponse<string>>
                            {
                                public AddUserDTO AddUserDTO { get; set; }
                                public AddUserCommand(AddUserDTO addUserDTO)
                                {
                                    AddUserDTO = addUserDTO;
                                }
                            }
                        }
                        