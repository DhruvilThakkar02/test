﻿using FluentValidation;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;

namespace HRMS.Utility.Validators.Tenant.Company
{
    public class CompanyDeleteRequestValidator : AbstractValidator<CompanyDeleteRequestDto>
    {
        public CompanyDeleteRequestValidator() 
        {
            RuleFor(x => x.CompanyId)
                .NotNull().WithMessage("Company Id is Required.")
                .GreaterThan(0).WithMessage("Company Id must be greater than Zero.");
        }
    }
}
