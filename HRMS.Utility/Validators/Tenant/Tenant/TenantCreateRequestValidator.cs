using FluentValidation;
using HRMS.Dtos.Tenant.Tenant.TenantRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Tenant
{
    public class TenantCreateRequestValidator : AbstractValidator<TenantCreateRequestDtos>
    {
        public TenantCreateRequestValidator()
        {
            RuleFor(tenant => tenant.OrganizationId)
              .NotNull().WithMessage("Organization Id is Required.")
            .GreaterThan(0).WithMessage("Organization Id must be greater than Zero.");

            RuleFor(tenant => tenant.DomainId)
              .NotNull().WithMessage("Domain Id is Required.")
              .GreaterThan(0).WithMessage("Domain Id must be greater than Zero.");

            RuleFor(tenant => tenant.SubdomainId)
              .NotNull().WithMessage("Subdomain Id is Required.")
            .GreaterThan(0).WithMessage("Subdomain Id must be greater than Zero.");

            RuleFor(tenant => tenant.TenantName)
                 .NotEmpty().WithMessage("Tenant Name is Required.")
                 .Length(2, 50).WithMessage("Tenant Name must be between 2 and 50 characters.");

            RuleFor(tenant => tenant.CreatedBy)
             .NotNull().WithMessage("CreatedBy is Required.")
             .GreaterThan(0).WithMessage("CreatedBy must be greater than Zero.");

            RuleFor(tenant => tenant.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
