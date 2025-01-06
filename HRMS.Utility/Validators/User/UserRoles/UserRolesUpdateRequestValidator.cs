using FluentValidation;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;

namespace HRMS.Utility.Validators.User.UserRoles
{
    public class UserRolesUpdateRequestValidator : AbstractValidator<UserRolesUpdateRequestDto>
    {
        public UserRolesUpdateRequestValidator()
        {
            RuleFor(roles => roles.UserRoleId)
               .NotNull().WithMessage("User Role ID is Required.")
               .GreaterThan(0).WithMessage("User Role ID must be greater than Zero.");

            RuleFor(roles => roles.UserRoleName)
           .NotEmpty().WithMessage("User Role Name Is Required")
           .Length(0, 100).WithMessage("User Role Name must be between 0 and 100 characters.");

            RuleFor(roles => roles.PermissionGroupId)
              .NotNull().WithMessage("Permission Group ID is Required.")
              .GreaterThan(0).WithMessage("Permission Group ID must be greater than Zero.");

            RuleFor(roles => roles.UpdatedBy)
              .NotNull().WithMessage("UpdatedBy is Required.")
              .GreaterThan(0).WithMessage("Updated By must be greater than Zero.");

            RuleFor(roles => roles.IsActive)
               .NotNull().WithMessage("IsActive must be true or false.");

            RuleFor(roles => roles.IsDelete)
              .NotNull().WithMessage("IsDelete must be true or false.");
        }
    }
}
