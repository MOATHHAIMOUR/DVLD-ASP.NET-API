using DVLD.Application.Common.ApiResponse;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.Features.PersonFeature.Command.DeletePerson
{
    public class DeletePersonCommand : IRequest<ApiResponse<string>>
    {        
        public int PersonId { get; set; }   
        public DeletePersonCommand(int personId)
        {
            PersonId = personId;
        }
    }
}
