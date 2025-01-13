using FluentValidation;
using HRMS.Dtos.Address.State.StateRequestDtos;

namespace HRMS.Utility.Validators.Address.State
{
    public class StateDeleteRequestValidator: AbstractValidator<StateDeleteRequestDto>
    {
        public StateDeleteRequestValidator()
        {
            RuleFor(x => x.StateId)
               .NotNull().WithMessage("State Id is Required.")
               .GreaterThan(0).WithMessage("State Id must be greater than Zero.");
        }
    }
}
