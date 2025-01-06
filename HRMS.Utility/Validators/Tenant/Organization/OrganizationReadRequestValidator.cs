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

            RuleFor(org => org.OrganizationName)
                .Length(2, 100).WithMessage("Organization Name must be between 2 and 100 characters.")
                .When(org => !string.IsNullOrEmpty(org.OrganizationName));



            RuleFor(org => org.CreatedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future.");


        }
    }
}
