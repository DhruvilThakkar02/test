using FluentValidation;
using HRMS.Dtos.User.User.UserRequestDtos;

namespace HRMS.Utility.Validators.User.User
{
    public class UserReadRequestValidator : AbstractValidator<UserReadRequestDto>
    {
        public UserReadRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("User Id is Required.")
                .GreaterThan(0).WithMessage("User Id must be greater than Zero.");
        }
    }
}
