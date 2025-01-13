using FluentValidation;
using HRMS.Dtos.User.UserRole.UserRoleRequestDtos;

namespace HRMS.Utility.Validators.User.UserRole
{
    public class UserRoleDeleteRequestValidator : AbstractValidator<UserRoleDeleteRequestDto>
    {
        public UserRoleDeleteRequestValidator()
        {

            RuleFor(roles => roles.UserRoleId)
               .NotNull().WithMessage("User Role ID is Required.")
               .GreaterThan(0).WithMessage("User Role ID must be greater than Zero.");
        }
    }
}
