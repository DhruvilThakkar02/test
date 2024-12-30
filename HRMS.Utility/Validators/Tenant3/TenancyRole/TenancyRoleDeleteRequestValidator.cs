﻿using FluentValidation;
using HRMS.Dtos.Tenant3.TenancyRole.TenancyRoleRequestDtos;

namespace HRMS.Utility.Validators.Tenant3.TenancyRole
{
    public class TenancyRoleDeleteRequestValidator : AbstractValidator<TenancyRoleDeleteRequestDto>
    {
        public TenancyRoleDeleteRequestValidator()
        {
            RuleFor(x => x.TenancyRoleID)
                .NotNull().WithMessage("Tenancy Role ID is Required.")
                .GreaterThan(0).WithMessage("Tenancy Role ID must be greater than Zero.");
        }
    }
}
