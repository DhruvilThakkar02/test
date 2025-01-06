using FluentValidation;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using System.Data;

namespace HRMS.Utility.Validators.User.UserRolesMapping
{
    public class UserRoleMappingUpdateRequestValidatorcs : AbstractValidator<UserRoleMappingUpdateRequestDto>
    {
        public UserRoleMappingUpdateRequestValidatorcs()
        {

            RuleFor(roles => roles.UserRoleMappingId)
                .NotNull().WithMessage("User Role Mapping Id is Required.")
                .GreaterThan(0).WithMessage("User Role Mapping Id must be greater than Zero.");

            RuleFor(roles => roles.UserId)
                 .NotNull().WithMessage("User Id is Required.")
                 .GreaterThan(0).WithMessage("User Id must be greater than Zero.");

            RuleFor(roles => roles.UserRoleId)
                  .NotNull().WithMessage("Role Id is Required.")
                  .GreaterThan(0).WithMessage("Role Id must be greater than Zero.");

            RuleFor(roles => roles.UpdatedBy)
                  .NotNull().WithMessage("Created By Id is Required.")
                  .GreaterThan(0).WithMessage("Created By must be greater than Zero.");
        }
       

    }
}
