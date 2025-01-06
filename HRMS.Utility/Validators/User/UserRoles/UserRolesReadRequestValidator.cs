using FluentValidation;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;

namespace HRMS.Utility.Validators.User.UserRoles
{
    public class UserRolesReadRequestValidator : AbstractValidator<UserRolesReadRequestDto>
    {
        public UserRolesReadRequestValidator()
        {
            RuleFor(roles => roles.UserRoleId)
              .NotNull().WithMessage("User Role ID is Required.")
              .GreaterThan(0).WithMessage("User Role ID must be greater than Zero.");
        }
    }
}
