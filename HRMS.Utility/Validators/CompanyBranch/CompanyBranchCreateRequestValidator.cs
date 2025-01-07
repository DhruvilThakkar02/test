using FluentValidation;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;

namespace HRMS.Utility.Validators.CompanyBranch
{
    public class CompanyBranchCreateRequestValidator : AbstractValidator<CompanyBranchCreateRequestDto>
    {
        public CompanyBranchCreateRequestValidator()
        {
            RuleFor(branch => branch.CompanyBranchName)
                .NotEmpty().WithMessage("CompanyBranchNameIsRequired.")
                .Length(2, 100).WithMessage("CompanyBranchName Must Be Between 2 And 100 Characters.");

            RuleFor(branch => branch.CompanyBranchHead)
                .NotEmpty().WithMessage("CompanyBranchHeadIsRequired.")
                .Length(2, 100).WithMessage("CompanyBranchHead Must Be Between 2 And 100 Characters.");

            RuleFor(branch => branch.ContactNumber)
                .Matches(@"^\+?\d{10,15}$").WithMessage("ContactNumber Must Be A Valid PhoneNumber.");

            RuleFor(branch => branch.Email)
                .EmailAddress().WithMessage("Email Must Be A Valid EmailAddress.");

            RuleFor(branch => branch.AddressId)
                .GreaterThan(0).WithMessage("Address Id Must Be A Valid Address.");

            RuleFor(branch => branch.AddressTypeId)
                .GreaterThan(0).WithMessage("AddressTypeId Must Be A Valid AddressType.");

            RuleFor(branch => branch.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId Must Be A Valid Company.");

            RuleFor(branch => branch.CreatedBy)
                .GreaterThan(0).WithMessage("CreatedBy Must Be A Valid UserId.");

            RuleFor(branch => branch.IsActive)
                .NotNull().WithMessage("IsActive Is Required.");
        }
    }
}
