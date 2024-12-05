using DVLD.Application.Features.PersonFeature.Queries.GetAllPersons;
using FluentValidation;

public class GetPeopleQueryValidator : AbstractValidator<GetAllPersonsQuery>
{
    public GetPeopleQueryValidator()
    {

        // NationalNo: Optional, max length 10
        RuleFor(x => x.PeopleSearchParams.NationalNo)
            .MaximumLength(10)
            .WithMessage("NationalNo must be at most 10 characters.");

        // FirstName: Optional, max length 20
        RuleFor(x => x.PeopleSearchParams.FirstName)
            .MaximumLength(20)
            .WithMessage("FirstName must be at most 20 characters.");

        // SecondName: Optional, max length 20
        RuleFor(x => x.PeopleSearchParams.SecondName)
            .MaximumLength(20)
            .WithMessage("SecondName must be at most 20 characters.");

        // ThirdName: Optional, max length 20
        RuleFor(x => x.PeopleSearchParams.ThirdName)
            .MaximumLength(20)
            .WithMessage("ThirdName must be at most 20 characters.");

        // LastName: Optional, max length 20
        RuleFor(x => x.PeopleSearchParams.LastName)
            .MaximumLength(20)
            .WithMessage("LastName must be at most 20 characters.");

        // Gender: Optional, but if present, must be 'M' or 'F'
        RuleFor(x => x.PeopleSearchParams.Gender)
            .Must(g => g == "Male" || g == "Female")
            .When(x => !string.IsNullOrEmpty(x.PeopleSearchParams.Gender))
            .WithMessage("Gender must be 'Male' or 'Female'.");

        // Phone: Optional, max length 15, validate format if required
        RuleFor(x => x.PeopleSearchParams.Phone)
            .MaximumLength(10)
            .WithMessage("Phone number must be at most 10 characters.");

        // Email: Optional, max length 50, validate email format if present
        RuleFor(x => x.PeopleSearchParams.Email)
            .MaximumLength(30)
            .WithMessage("Email must be at most 30 characters.")
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.PeopleSearchParams.Email))
            .WithMessage("Email must be a valid email address.");

        // CountryName: Optional, max length 50
        RuleFor(x => x.PeopleSearchParams.CountryName)
            .MaximumLength(50)
            .WithMessage("CountryName must be at most 50 characters.");

        // OrderBy: Optional, max length 50
        RuleFor(x => x.PeopleSearchParams.OrderBy)
            .MaximumLength(50)
            .WithMessage("SortBy must be at most 50 characters.")
            .Must(sortBy => IsValidProperty(sortBy))
            .When(x => !string.IsNullOrEmpty(x.PeopleSearchParams.OrderBy))
            .WithMessage("SortBy must be a valid property of PeopleSearchParams.");

        // OrderDirection: Optional, but must be 'ASC' or 'DESC' if specified
        RuleFor(x => x.PeopleSearchParams.OrderDirection)
            .Must(direction => direction == "ASC" || direction == "DESC")
            .When(x => !string.IsNullOrEmpty(x.PeopleSearchParams.OrderBy))
            .WithMessage("OrderDirection must be 'ASC' or 'DESC'.");

        // PageSize: Must be between 1 and 100 (for example)
        RuleFor(x => x.PeopleSearchParams.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");

        // PageNumber: Must be greater than 0
        RuleFor(x => x.PeopleSearchParams.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

    }

    private bool IsValidProperty(string? orderBy)
    {

        // Define a list of valid property names
        var validProperties = new List<string>
{
    "PersonId",
    "NationalNo",
    "FirstName",
    "SecondName",
    "ThirdName",
    "LastName",
    "Gender",
    "Phone",
    "Email",
    "Address",
    "DateOfBirth",
    "Nationality"
};
        // Check if orderBy is one of the valid properties
        return validProperties.Contains(orderBy);
    }

}
