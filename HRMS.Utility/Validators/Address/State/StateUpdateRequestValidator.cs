using FluentValidation;
using HRMS.Dtos.Address.State.StateRequestDtos;

namespace HRMS.Utility.Validators.Address.State
{
    public class StateUpdateRequestValidator : AbstractValidator<StateUpdateRequestDto>
    {
        public StateUpdateRequestValidator()
        {
            RuleFor(x => x.StateId)
               .GreaterThan(0).WithMessage("State Id must be greater than 0."); 

            RuleFor(x => x.StateName)
                .NotEmpty().WithMessage("State name is required.")
                .Length(2, 100).WithMessage("State name must be between 2 and 100 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be specified.");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required.");
        }
    }
}
