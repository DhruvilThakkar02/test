using FluentValidation;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;

namespace HRMS.Utility.Validators.CompanyBranch
{
    public class CompanyBranchDeleteRequestValidator : AbstractValidator<CompanyBranchDeleteRequestDto>
    {
        public CompanyBranchDeleteRequestValidator()
        {
            RuleFor(branch => branch.CompanyBranchId)
                .GreaterThan(0).WithMessage("CompanyBranchId Must Be A Valid Id.");
        }
    }
}
