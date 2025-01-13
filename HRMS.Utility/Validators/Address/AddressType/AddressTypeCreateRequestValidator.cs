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
    public class AddressTypeCreateRequestValidator: AbstractValidator<AddressTypeCreateRequestDto>
    {
        public AddressTypeCreateRequestValidator()
        {
            RuleFor(addresstype => addresstype.AddressTypeName)
                .NotEmpty().WithMessage("AddressTypeName is Required.")
                .Length(2, 50).WithMessage("AddressTypeName must be between 2 and 50 characters.");

          


            RuleFor(addresstype => addresstype.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

           

          

        }
    }
}
