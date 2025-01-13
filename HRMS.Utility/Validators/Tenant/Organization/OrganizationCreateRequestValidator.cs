using FluentValidation;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Organization
{
    public class OrganizationCreateRequestValidator : AbstractValidator<OrganizationCreateRequestDto>
    {
        public OrganizationCreateRequestValidator()
        {
            RuleFor(org => org.OrganizationName)
             .NotEmpty().WithMessage("Organization Name is required.")
             .Length(2, 100).WithMessage("Organization Name must be between 2 and 100 characters.");

            RuleFor(org => org.CreatedBy)
                .GreaterThan(0).WithMessage("CreatedBy must be a valid User Id.");
        }

    }
}
