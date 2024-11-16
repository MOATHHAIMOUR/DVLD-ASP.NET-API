using AutoMapper;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Command.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, ApiResponse<string>>
    {
        private readonly IPersonServices _personServices;
        private readonly IMapper _mapper; 
        public AddPersonCommandHandler(IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {

            byte[] imageBytes = [];

            // convert image to bytes if exist
            if (request.AddPersonDTO.Image != null && request.AddPersonDTO.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.AddPersonDTO.Image.CopyToAsync(memoryStream);

                    imageBytes = memoryStream.ToArray();
                }
            }

            Person person = new Person()
            {
                PersonId = 0,
                NationalNo = request.AddPersonDTO.NationalNo,
                Address = request.AddPersonDTO.Address, 
                CountryId = request.AddPersonDTO.CountryId,
                DateOfBirth= request.AddPersonDTO.DateOfBirth,
                Email = request.AddPersonDTO.Email, 
                FirstName = request.AddPersonDTO.FirstName, 
                SecondName = request.AddPersonDTO.LastName,
                ThirdName = request.AddPersonDTO.ThirdName, 
                LastName = request.AddPersonDTO.LastName,   
                Gender= request.AddPersonDTO.Gender == "Male"? EnumGender.Male:EnumGender.Female,
                Phone = request.AddPersonDTO.Phone, 
                ImageBytes = imageBytes,    
            };

            var result = await _personServices.AddPersonAsync(person);
          
            return ApiResponseHandler.Success<string>(
                   message: $"{request.AddPersonDTO.FirstName} has been added successfully with ID {result.Value}"
               );
        }
    }
}
