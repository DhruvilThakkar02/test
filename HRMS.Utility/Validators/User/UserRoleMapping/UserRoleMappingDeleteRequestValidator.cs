using FluentValidation;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;
using System.Data;

namespace HRMS.Utility.Validators.User.UserRoleMapping
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
