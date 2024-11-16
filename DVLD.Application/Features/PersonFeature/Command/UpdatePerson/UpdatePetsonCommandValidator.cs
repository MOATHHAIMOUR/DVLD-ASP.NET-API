﻿using FluentValidation;

namespace DVLD.Application.Features.PersonFeature.Command.UpdatePerson
{
    public class UpdatePetsonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePetsonCommandValidator()
        {
            // NationalNo: Required, max length 10
            RuleFor(x => x.UpdatePersonDTO.NationalNo)
                .NotEmpty()
                .WithMessage("NationalNo is required.")
                .MaximumLength(10)
                .WithMessage("NationalNo must be at most 10 characters.");

            // FirstName: Required, max length 20
            RuleFor(x => x.UpdatePersonDTO.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MaximumLength(20)
                .WithMessage("FirstName must be at most 20 characters.");

            // SecondName: Required, max length 20
            RuleFor(x => x.UpdatePersonDTO.SecondName)
                .NotEmpty()
                .WithMessage("SecondName is required.")
                .MaximumLength(20)
                .WithMessage("SecondName must be at most 20 characters.");

            // ThirdName: Required, max length 20
            RuleFor(x => x.UpdatePersonDTO.ThirdName)
                .NotEmpty()
                .WithMessage("ThirdName is required.")
                .MaximumLength(20)
                .WithMessage("ThirdName must be at most 20 characters.");

            // LastName: Required, max length 20
            RuleFor(x => x.UpdatePersonDTO.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MaximumLength(20)
                .WithMessage("LastName must be at most 20 characters.");

            // Gender: Required, must be 'Male' or 'Female'
            RuleFor(x => x.UpdatePersonDTO.Gender)
                .NotEmpty()
                .WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be 'Male' or 'Female'.");

            // Phone: Required, max length 10
            RuleFor(x => x.UpdatePersonDTO.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(10)
                .WithMessage("Phone number must be at most 10 characters.");

            // Email: Required, max length 30, validate email format
            RuleFor(x => x.UpdatePersonDTO.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .MaximumLength(30)
                .WithMessage("Email must be at most 30 characters.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.");

            // CountryId: Required, valid between 1 and 198
            RuleFor(x => x.UpdatePersonDTO.CountryId)
                .NotEmpty()
                .WithMessage("CountryId is required.")
                .InclusiveBetween(1, 198)
                .WithMessage("CountryId must be between 1 and 198.");
        }
    }
}