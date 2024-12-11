using FluentValidation;

namespace DVLD.Application.Features.PersonFeature.Queries.GetPerson
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    if (!x.PersonId.HasValue && string.IsNullOrEmpty(x.NationalNo))
                        // the error will raise
                        return false;
                    // no error
                    return true;
                })
                .WithMessage("You should provide at least one value for either PersonId or NationalNo.");
        }
    }
}
