using FluentValidation;
using HRMS.Dtos.Tenant.Tenant.TenantRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Tenant
{
    public class TenantUpdateRequestValidator : AbstractValidator<TenantUpdateRequestDtos>
    {
        public TenantUpdateRequestValidator()
        {
            RuleFor(tenant => tenant.TenantId)
               .NotNull().WithMessage("Tenant ID is Required.")
               .GreaterThan(0).WithMessage("Tenant ID must be greater than Zero.");

            RuleFor(tenant => tenant.OrganizationId)
                  .NotNull().WithMessage("Organization ID is Required.")
                  .GreaterThan(0).WithMessage("Organization ID must be greater than Zero.");

            RuleFor(tenant => tenant.DomainId)
              .NotNull().WithMessage("Domain ID is Required.")
              .GreaterThan(0).WithMessage("Domain ID must be greater than Zero.");

            RuleFor(tenant => tenant.SubdomainId)
              .NotNull().WithMessage("Subdomain ID is Required.")
              .GreaterThan(0).WithMessage("Subdomain ID must be greater than Zero.");

            RuleFor(tenant => tenant.TenantName)
                 .NotEmpty().WithMessage("Tenant Name is Required.")
                 .Length(2, 50).WithMessage("Tenant Name must be between 2 and 50 characters.");

            RuleFor(tenant => tenant.UpdatedBy)
             .NotNull().WithMessage("UpdatedBy is Required.")
             .GreaterThan(0).WithMessage("UpdatedBy must be greater than Zero.");

            RuleFor(tenant => tenant.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

            RuleFor(tenant => tenant.IsDelete)
              .NotNull().WithMessage("IsDelete must be true or false.");
        }
    }
}
