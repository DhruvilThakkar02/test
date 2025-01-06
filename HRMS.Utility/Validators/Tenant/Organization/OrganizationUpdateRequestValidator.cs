using FluentValidation;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Organization
{
    public class OrganizationUpdateRequestValidator : AbstractValidator<OrganizationUpdateRequestDto>
    {
        public OrganizationUpdateRequestValidator()
        {

            RuleFor(org => org.OrganizationId)
                .GreaterThan(0).WithMessage("OrganizationId must be greater than Zero.");

            RuleFor(org => org.OrganizationName)
                .Length(2, 100).WithMessage("Organization Name must be between 2 and 100 characters.")
                .When(org => !string.IsNullOrEmpty(org.OrganizationName));

            RuleFor(org => org.UpdatedBy)
                .GreaterThan(0).WithMessage("UpdatedBy must be a valid User Id.");
        }
    }
}
