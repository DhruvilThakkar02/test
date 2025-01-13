using FluentValidation;
using HRMS.Dtos.Address.City.CityRequestDtos;

namespace HRMS.Utility.Validators.Address.City
{
    public class CityUpdateRequestValidator: AbstractValidator<CityUpdateRequestDto>
    {
        public CityUpdateRequestValidator()
        {
            RuleFor(city => city.CityId)
               .GreaterThan(0).WithMessage("City ID must be a positive integer.");

            RuleFor(city => city.CityName)
                .NotEmpty().WithMessage("City Name is Required.")
                .Length(2, 100).WithMessage("City Name must be between 2 and 100 characters.");

            RuleFor(city => city.UpdatedBy)
                .NotEmpty().WithMessage("Updated By is Required.")
                .GreaterThan(0).WithMessage("Updated By must be a positive integer.");

            RuleFor(city => city.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

            RuleFor(city => city.IsDelete)
                .NotNull().WithMessage("IsDelete must be true or false.");

            RuleFor(city => city.UpdatedAt)
                .NotEmpty().WithMessage("Updated At is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Updated At cannot be in the future.");
        }
    }
}
