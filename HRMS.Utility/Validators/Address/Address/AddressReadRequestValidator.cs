using FluentValidation;
using HRMS.Dtos.Address.Address.AddressRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Validators.Address.Address
{
    public class AddressReadRequestValidator:AbstractValidator<AddressReadRequestDto>
    {
        public AddressReadRequestValidator()
        {
            RuleFor(x => x.AddressId)
                .NotNull().WithMessage("AddressId is Required.")
                .GreaterThan(0).WithMessage("AddressId must be greater than Zero.");
        }
    }
}
