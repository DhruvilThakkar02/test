using FluentValidation;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;

namespace HRMS.Utility.Validators.User.UserRoleMapping
{
    public class UserRoleMappingReadRequestValidator : AbstractValidator<UserRoleMappingReadRequestDto>
    {

        public UserRoleMappingReadRequestValidator() 
        {
            RuleFor(roles => roles.UserRoleMappingId)
              .NotNull().WithMessage("User Role Mapping Id is Required.")
              .GreaterThan(0).WithMessage("User Role Mapping Id must be greater than Zero.");
        }
    }
}
