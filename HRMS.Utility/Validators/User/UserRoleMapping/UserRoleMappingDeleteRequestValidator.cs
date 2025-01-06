using FluentValidation;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using System.Data;

namespace HRMS.Utility.Validators.User.UserRolesMapping
{
    public class UserRoleMappingDeleteRequestValidator : AbstractValidator<UserRoleMappingDeleteRequestDto>
    {

       
        public UserRoleMappingDeleteRequestValidator() 
        {

            RuleFor(roles => roles.UserRoleMappingId)
              .NotNull().WithMessage("User Role Mapping Id is Required.")
              .GreaterThan(0).WithMessage("User Role Mapping Id must be greater than Zero.");
        }

        
    }
}
