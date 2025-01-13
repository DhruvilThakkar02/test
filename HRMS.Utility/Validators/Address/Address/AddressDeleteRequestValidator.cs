using FluentValidation;
using HRMS.Dtos.Address.Address.AddressRequestDtos;

namespace HRMS.Utility.Validators.Address.Address
{
    public class AddressDeleteRequestValidator : AbstractValidator<AddressDeleteRequestDto>
    {
        public AddressDeleteRequestValidator()
        {
            RuleFor(x => x.AddressId)
               .NotNull().WithMessage("AddressId is Required.")
               .GreaterThan(0).WithMessage("AddressId must be greater than Zero.");
        }
    }
}
