using FluentValidation;
using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Validators.Address.AddressType
{
    public class AddressTypeReadRequestValidator: AbstractValidator<AddressTypeReadRequestDto>
    {
        public AddressTypeReadRequestValidator()
        {
            RuleFor(x => x.AddressTypeId)
             .NotNull().WithMessage("AddressTypeId is Required.")
             .GreaterThan(0).WithMessage("AddressTypeId must be greater than Zero.");
        }
    }
}
