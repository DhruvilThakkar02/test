using FluentValidation;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;

namespace HRMS.Utility.Validators.Address.Country
{
    public class CountryReadRequestValidator : AbstractValidator<CountryReadRequestDto>
    {
        public CountryReadRequestValidator()
        {

            RuleFor(x => x.CountryId).NotNull().WithMessage("CountryId is Required.")
                .GreaterThan(0).WithMessage("CountryId must be greater than Zero.");


        }

    }
}
