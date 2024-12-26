﻿using FluentValidation;
using HRMS.Dtos.Tenant3.TenancyRole.TenancyRoleRequestDtos;

namespace HRMS.Utility.Validators.Tenant3.TenancyRole
{
    public class TenancyRoleCreateRequestValidator : AbstractValidator<TenancyRoleCreateRequestDto>
    {
        public TenancyRoleCreateRequestValidator()
        {
            RuleFor(tenancyRole => tenancyRole.RoleName)
                .NotEmpty().WithMessage("Tenancy Role Name is Required.")
                .Length(2, 100).WithMessage("Tenancy Role Name must be between 2 and 100 characters.");

            RuleFor(tenancyRole => tenancyRole.CreatedBy)
              .GreaterThan(0).WithMessage("CreatedBy must be greater than 0.");

            RuleFor(tenancyRole => tenancyRole.CreatedAt)
                .NotEmpty().WithMessage("Created At is Required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created At cannot be in the future.");

            RuleFor(tenancyRole => tenancyRole.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
