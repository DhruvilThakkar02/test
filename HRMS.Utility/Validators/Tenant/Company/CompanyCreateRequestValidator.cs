using FluentValidation;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Company
{
    public class CompanyCreateRequestValidator : AbstractValidator<CompanyCreateRequestDto>
    {
        public CompanyCreateRequestValidator() 
        {
            RuleFor(company => company.CompanyName)
              .NotEmpty().WithMessage("Company Name is Required.")
              .Length(2, 255).WithMessage("Company Name must be between 2 and 255 characters.");

            RuleFor(company => company.Industry)
                .NotEmpty().WithMessage("Industry is Required.")
                .Length(2, 255).WithMessage("Industry must be between 2 and 255 characters.");

            RuleFor(company => company.CompanyType)
                .NotEmpty().WithMessage("Company Type is Required.")
                .Length(2, 255).WithMessage("Company Type must be between 2 and 255 characters.");

            RuleFor(company => company.FoundedDate)
                .NotEmpty().WithMessage("Founded Date is Required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Founded Date cannot be in the future.");

            RuleFor(company => company.NumberOfEmployees)
                .GreaterThanOrEqualTo(0).WithMessage("Number of Employees must be a non-negative number.");

            RuleFor(company => company.WebsiteUrl)
                .NotEmpty().WithMessage("Website URL is Required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Invalid Website URL format.");

            RuleFor(company => company.TaxNumber)
                .NotEmpty().WithMessage("Tax Number is Required.")
                .Length(2, 50).WithMessage("Tax Number must be between 2 and 50 characters.");

            RuleFor(company => company.GstNumber)
                .NotEmpty().WithMessage("GST Number is Required.")
                .Length(2, 50).WithMessage("GST Number must be between 2 and 50 characters.");

            RuleFor(company => company.PfNumber)
                .NotEmpty().WithMessage("PF Number is Required.")
                .Length(2, 50).WithMessage("PF Number must be between 2 and 50 characters.");

            RuleFor(company => company.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is Required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid Phone Number format.");

            RuleFor(company => company.Logo)
                .NotNull().WithMessage("Logo is Required.");

            RuleFor(company => company.Email)
                .NotEmpty().WithMessage("Email is Required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(company => company.AddressId)
                .NotNull().WithMessage("Address ID is Required.");

            RuleFor(company => company.TenantId)
                .NotNull().WithMessage("Tenant ID is Required.");

            RuleFor(company => company.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
