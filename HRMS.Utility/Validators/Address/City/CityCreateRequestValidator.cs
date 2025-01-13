using FluentValidation;
using HRMS.Dtos.Address.City.CityRequestDtos;

namespace HRMS.Utility.Validators.Address.City
{
    public class CityCreateRequestValidator : AbstractValidator<CityCreateRequestDto>
    {
        public CityCreateRequestValidator()
        {
            RuleFor(city => city.CityName)
               .NotEmpty().WithMessage("City Name is Required.")
               .Length(2, 100).WithMessage("City Name must be between 2 and 100 characters.");

            RuleFor(city => city.CreatedBy)
                .NotEmpty().WithMessage("Created By is Required.")
                .GreaterThan(0).WithMessage("Created By must be a positive integer.");

            RuleFor(city => city.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");
        }
    }
}
