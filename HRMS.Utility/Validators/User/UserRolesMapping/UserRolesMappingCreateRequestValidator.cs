using FluentValidation;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;

namespace HRMS.Utility.Validators.User.UserRolesMapping
{
    public class UserRolesMappingCreateRequestValidator : AbstractValidator<UserRolesMappingCreateRequestDto>
    {
        public UserRolesMappingCreateRequestValidator()
        {
            RuleFor(roles => roles.UserId)
              .NotNull().WithMessage("User Id is Required.")
              .GreaterThan(0).WithMessage("User Id must be greater than Zero.");

            RuleFor(roles => roles.RoleId)
              .NotNull().WithMessage("Role Id is Required.")
              .GreaterThan(0).WithMessage("Role Id must be greater than Zero.");

            RuleFor(roles => roles.CreatedBy)
              .NotNull().WithMessage("Created By Id is Required.")
              .GreaterThan(0).WithMessage("Created By must be greater than Zero.");
        }
    }
}
