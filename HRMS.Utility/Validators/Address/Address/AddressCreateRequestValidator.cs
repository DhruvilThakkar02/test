using FluentValidation;
using HRMS.Dtos.Address.Address.AddressRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Validators.Address.Address
{
    public class AddressCreateRequestValidator: AbstractValidator<AddressCreateRequestDto>
    {
        public AddressCreateRequestValidator()
        {
            RuleFor(address => address.AddressLine1)
               .NotEmpty().WithMessage("AddressLine1 is Required.")
               .Length(2, 50).WithMessage("AddressLine1 must be between 2 and 100 characters.");

            RuleFor(address => address.AddressLine2)
               .NotEmpty().WithMessage("AddressLine2 is Required.")
               .Length(2, 50).WithMessage("AddressLine2 must be between 2 and 100 characters.");

            RuleFor(address => address.CityId)
               .NotNull().WithMessage("CityId is Required.");

            RuleFor(address => address.StateId)
               .NotNull().WithMessage("StateId is Required.");

            RuleFor(address => address.CountryId)
               .NotNull().WithMessage("CountryId is Required.");

            RuleFor(address => address.PostalCode)
               .NotNull().WithMessage("PostalCode is Required.");
            
            RuleFor(address => address.AddressTypeId)
               .NotNull().WithMessage("AddressTypeId is Required.");

            RuleFor(address => address.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

        

        }
    }
}
