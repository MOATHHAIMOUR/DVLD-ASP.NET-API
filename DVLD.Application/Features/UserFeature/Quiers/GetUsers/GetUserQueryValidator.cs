using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUsers
{
    public class GetUserQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserSearchParamsDTO.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.")
                .When(x => x.UserSearchParamsDTO.UserId.HasValue);

            RuleFor(x => x.UserSearchParamsDTO.PersonId)
                .GreaterThan(0).WithMessage("PersonId must be greater than 0.")
                .When(x => x.UserSearchParamsDTO.PersonId.HasValue);

            RuleFor(x => x.UserSearchParamsDTO.UserName)
                .NotEmpty().WithMessage("UserName must not be empty.")
                .MaximumLength(20).WithMessage("UserName must not exceed 20 characters.")
                .When(x => !string.IsNullOrEmpty(x.UserSearchParamsDTO.UserName));

            RuleFor(x => x.UserSearchParamsDTO.SortBy)
                .Must(sortBy => IsValidSortField(sortBy))
                .WithMessage("Invalid SortBy field.")
                .When(x => !string.IsNullOrEmpty(x.UserSearchParamsDTO.SortBy));

            RuleFor(x => x.UserSearchParamsDTO.SortDireaction)
                .Must(sortDirection =>
                    sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                    sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("SortDireaction must be 'asc' or 'desc'.")
                .When(x => !string.IsNullOrEmpty(x.UserSearchParamsDTO.SortDireaction));

            RuleFor(x => x.UserSearchParamsDTO.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("PageSize must not exceed 100.")
                .When(x => x.UserSearchParamsDTO.PageSize.HasValue);

            RuleFor(x => x.UserSearchParamsDTO.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.")
                .When(x => x.UserSearchParamsDTO.PageNumber.HasValue);
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
