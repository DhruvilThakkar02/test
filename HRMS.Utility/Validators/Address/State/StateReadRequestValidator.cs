using FluentValidation;
using HRMS.Dtos.Address.State.StateRequestDtos;

namespace HRMS.Utility.Validators.Address.State
{
    public class StateReadRequestValidator : AbstractValidator<StateReadRequestDto>
    {
        public StateReadRequestValidator()
        {
            RuleFor(x => x.StateId)
              .NotNull().WithMessage("State Id is Required.")
              .GreaterThan(0).WithMessage("State Id must be greater than Zero.");
        }
    }
}
