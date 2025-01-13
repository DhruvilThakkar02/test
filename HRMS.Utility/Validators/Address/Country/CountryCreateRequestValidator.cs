using FluentValidation;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;

namespace HRMS.Utility.Validators.Address.Country
{
    public class CountryCreateRequestValidator : AbstractValidator<CountryCreateRequestDto>
    {
        public CountryCreateRequestValidator()
        {
            RuleFor(x => x.CountryName)
            .NotEmpty().WithMessage("Country name is required.")
            .Length(2, 100).WithMessage("Country name must be between 2 and 100 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be specified.");

            RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.");
        }
    }
}
