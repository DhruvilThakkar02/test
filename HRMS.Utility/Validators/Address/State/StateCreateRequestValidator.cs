using FluentValidation;
using HRMS.Dtos.Address.State.StateRequestDtos;

namespace HRMS.Utility.Validators.Address.State
{
    public class StateCreateRequestValidator: AbstractValidator<StateCreateRequestDto>
    {
        public StateCreateRequestValidator()
        {
            RuleFor(x => x.StateName)
               .NotEmpty().WithMessage("State name is required.")
               .Length(2, 100).WithMessage("State name must be between 2 and 100 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be specified.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");
        }
    }
}
