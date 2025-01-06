using FluentValidation;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;

namespace HRMS.Utility.Validators.User.UserRoles
{
    public class UserRolesCreateRequestValidator : AbstractValidator<UserRolesCreateRequestDto>
    {
        public UserRolesCreateRequestValidator()
        {

            RuleFor(roles => roles.UserRoleName)
            .NotEmpty().WithMessage("User Role Name Is Required")
            .Length(0, 100).WithMessage("User Role Name must be between 0 and 100 characters.");

            RuleFor(roles => roles.PermissionGroupId)
              .NotNull().WithMessage("Permission Group Id is Required.")
              .GreaterThan(0).WithMessage("Permission Group Id must be greater than Zero.");

            RuleFor(roles => roles.CreatedBy)
              .NotNull().WithMessage("CreatedBy is Required.")
              .GreaterThan(0).WithMessage("Created By must be greater than Zero.");

            RuleFor(roles => roles.IsActive)
               .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
