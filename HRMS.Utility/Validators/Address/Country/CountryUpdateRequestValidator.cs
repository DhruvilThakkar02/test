using FluentValidation;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;

namespace HRMS.Utility.Validators.Address.Country
{
    public class CountryUpdateRequestValidator : AbstractValidator<CountryUpdateRequestDto>
    {
        public CountryUpdateRequestValidator()
        {
            RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("CountryId must be greater than Zero.");

            RuleFor(x => x.CountryName)
                .Length(2, 100).When(x => !string.IsNullOrEmpty(x.CountryName))
                .WithMessage("Country name must be between 2 and 100 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be a valid boolean value (true/false).");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required.");

        }
    }
}
