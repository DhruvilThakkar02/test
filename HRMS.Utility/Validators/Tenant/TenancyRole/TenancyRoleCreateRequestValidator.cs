using FluentValidation;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleRequestDtos;

namespace HRMS.Utility.Validators.Tenant.TenancyRole
{
    public class TenancyRoleCreateRequestValidator : AbstractValidator<TenancyRoleCreateRequestDto>
    {
        public TenancyRoleCreateRequestValidator()
        {
            RuleFor(tenancyRole => tenancyRole.TenancyRoleName)
                .NotEmpty().WithMessage("Tenancy Role Name is Required.")
                .Length(2, 100).WithMessage("Tenancy Role Name must be between 2 and 100 characters.");

            RuleFor(tenancyRole => tenancyRole.CreatedBy)
              .GreaterThan(0).WithMessage("CreatedBy must be greater than 0.");

            RuleFor(tenancyRole => tenancyRole.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
