using FluentValidation;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;

namespace HRMS.Utility.Validators.User.UserRoles
{
    public class UserRolesDeleteRequestValidator : AbstractValidator<UserRolesDeleteRequestDto>
    {
        public UserRolesDeleteRequestValidator()
        {

            RuleFor(roles => roles.UserRoleId)
               .NotNull().WithMessage("User Role ID is Required.")
               .GreaterThan(0).WithMessage("User Role ID must be greater than Zero.");
        }
    }
}
