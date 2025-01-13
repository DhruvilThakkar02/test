using FluentValidation;
using HRMS.Dtos.Address.City.CityRequestDtos;

namespace HRMS.Utility.Validators.Address.City
{
    public class CityReadRequestValidator : AbstractValidator<CityReadRequestDto>
    {
        public CityReadRequestValidator()
        {
            RuleFor(city => city.CityId)
               .NotNull().WithMessage("City Id is Required.")
               .GreaterThan(0).WithMessage("City Id must be greater than Zero.");
        }
    }
}
