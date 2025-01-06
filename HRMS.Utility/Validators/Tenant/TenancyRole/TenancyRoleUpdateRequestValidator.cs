using FluentValidation;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleRequestDtos;

namespace HRMS.Utility.Validators.Tenant.TenancyRole
{
    public class TenancyRoleUpdateRequestValidator : AbstractValidator<TenancyRoleUpdateRequestDto>
    {
        public TenancyRoleUpdateRequestValidator()
        {
            RuleFor(x => x.TenancyRoleId)
               .NotNull().WithMessage("Tenancy Role ID is Required.")
               .GreaterThan(0).WithMessage("Tenancy Role ID must be greater than Zero.");

            RuleFor(tenancyRole => tenancyRole.TenancyRoleName)
                .NotEmpty().WithMessage("Tenancy Role Name is Required.")
                .Length(2, 100).WithMessage("Tenancy Role Name must be between 2 and 100 characters.");

            RuleFor(tenancyRole => tenancyRole.UpdatedBy)
                .GreaterThan(0).WithMessage("UpdatedBy must be greater than 0.");

            RuleFor(tenancyRole => tenancyRole.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

            RuleFor(tenancyRole => tenancyRole.IsDelete)
            .NotNull().WithMessage("IsDelete must be true or false.");
        }
    }
}
