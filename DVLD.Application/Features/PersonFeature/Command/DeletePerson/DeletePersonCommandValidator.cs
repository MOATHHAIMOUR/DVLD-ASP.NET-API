﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.Features.PersonFeature.Command.DeletePerson
{
    public class AddPersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person Id is required")
                .GreaterThan(0).WithMessage("Person Id must be greater than zero");
        }
    }
}
