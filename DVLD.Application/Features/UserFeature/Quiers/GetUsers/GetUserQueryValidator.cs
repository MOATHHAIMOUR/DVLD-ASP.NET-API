using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUserQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UsersSearchParameters.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.")
                .When(x => x.UsersSearchParameters.UserId.HasValue);

            RuleFor(x => x.UsersSearchParameters.PersonId)
                .GreaterThan(0).WithMessage("PersonId must be greater than 0.")
                .When(x => x.UsersSearchParameters.PersonId.HasValue);

            RuleFor(x => x.UsersSearchParameters.UserName)
                .NotEmpty().WithMessage("UserName must not be empty.")
                .MaximumLength(20).WithMessage("UserName must not exceed 20 characters.")
                .When(x => !string.IsNullOrEmpty(x.UsersSearchParameters.UserName));

            RuleFor(x => x.UsersSearchParameters.SortBy)
                .Must(sortBy => IsValidSortField(sortBy))
                .WithMessage("Invalid SortBy field.")
                .When(x => !string.IsNullOrEmpty(x.UsersSearchParameters.SortBy));

            RuleFor(x => x.UsersSearchParameters.SortDirection)
                .Must(sortDirection =>
                    sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                    sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("SortDireaction must be 'asc' or 'desc'.")
                .When(x => !string.IsNullOrEmpty(x.UsersSearchParameters.SortDirection));

            RuleFor(x => x.UsersSearchParameters.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("PageSize must not exceed 100.")
                .When(x => x.UsersSearchParameters.PageSize.HasValue);

            RuleFor(x => x.UsersSearchParameters.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.")
                .When(x => x.UsersSearchParameters.PageNumber.HasValue);
        }

        private bool IsValidSortField(string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return true; // Allow null or empty values.

            // List of valid fields for sorting
            var validFields = new[] { "UserId", "UserName", "IsActive" };

            return validFields.Contains(sortBy);
        }
    }
}
