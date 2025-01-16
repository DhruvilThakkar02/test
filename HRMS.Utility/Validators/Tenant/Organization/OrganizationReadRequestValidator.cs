using FluentValidation;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Organization
{
    public class OrganizationReadRequestValidator : AbstractValidator<OrganizationReadRequestDto>
    {
        public OrganizationReadRequestValidator()
        {
            RuleFor(org => org.OrganizationId)
                .GreaterThan(0).WithMessage("OrganizationId must be greater than Zero.");


        }
    }
}
