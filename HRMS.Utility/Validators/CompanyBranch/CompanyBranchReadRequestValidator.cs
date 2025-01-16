using FluentValidation;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;

namespace HRMS.Utility.Validators.CompanyBranch
{
    public class CompanyBranchReadRequestValidator : AbstractValidator<CompanyBranchReadRequestDto>
    {
        public CompanyBranchReadRequestValidator()
        {
            RuleFor(branch => branch.CompanyBranchId)
                .GreaterThan(0).WithMessage("CompanyBranchIdMustBeAValidId.");
        }
    }
}
