using FluentValidation;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;

namespace HRMS.Utility.Validators.CompanyBranch
{
    public class CompanyBranchUpdateRequestValidator : AbstractValidator<CompanyBranchUpdateRequestDto>
    {
        public CompanyBranchUpdateRequestValidator()
        {
            RuleFor(branch => branch.CompanyBranchId)
                .GreaterThan(0).WithMessage("CompanyBranchIdMustBeAValidId.");

            RuleFor(branch => branch.CompanyBranchName)
                .NotEmpty().WithMessage("CompanyBranchNameIsRequired.")
                .Length(2, 100).WithMessage("CompanyBranchNameMustBeBetween2And100Characters.");

            RuleFor(branch => branch.CompanyBranchHead)
                .NotEmpty().WithMessage("CompanyBranchHeadIsRequired.")
                .Length(2, 100).WithMessage("CompanyBranchHeadMustBeBetween2And100Characters.");

            RuleFor(branch => branch.ContactNumber)
                .Matches(@"^\+?\d{10,15}$").WithMessage("ContactNumberMustBeAValidPhoneNumber.");

            RuleFor(branch => branch.Email)
                .EmailAddress().WithMessage("EmailMustBeAValidEmailAddress.");

            RuleFor(branch => branch.AddressId)
                .GreaterThan(0).WithMessage("AddressIdMustBeAValidAddress.");

            RuleFor(branch => branch.AddressTypeId)
                .GreaterThan(0).WithMessage("AddressTypeIdMustBeAValidAddressType.");

            RuleFor(branch => branch.CompanyId)
                .GreaterThan(0).WithMessage("CompanyIdMustBeAValidCompany.");

            RuleFor(branch => branch.UpdatedBy)
                .GreaterThan(0).WithMessage("UpdatedByMustBeAValidUserId.");

            RuleFor(branch => branch.IsActive)
                .NotNull().WithMessage("IsActiveIsRequired.");

            
        }
    }
}
