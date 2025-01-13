using FluentValidation;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;

namespace HRMS.Utility.Validators.Address.Country
{
    public class CountryDeleteRequestValidator : AbstractValidator<CountryDeleteRequestDto>
    {
        public CountryDeleteRequestValidator()
        {

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("CountryId must be greater than Zero.");
        }
    }
}
