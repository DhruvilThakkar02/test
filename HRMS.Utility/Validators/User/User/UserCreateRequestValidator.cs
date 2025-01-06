using FluentValidation;
using HRMS.Dtos.User.User.UserRequestDtos;

namespace HRMS.Utility.Validators.User.User
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequestDto>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First Name is Required.")
                .Length(2, 50).WithMessage("First Name must be between 2 and 50 characters.");

            RuleFor(user => user.MiddleName)
              .NotEmpty().WithMessage("Middle Name is Required.")
              .Length(0, 100).WithMessage("Middle Name must be between 0 and 100 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last Name is Required.")
                .Length(2, 50).WithMessage("Last Name must be between 2 and 50 characters.");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("User Name is Required.")
                .Length(2, 50).WithMessage("User Name must be between 2 and 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is Required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is Required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.Gender)
               .NotEmpty().WithMessage("Gender is Required.")
               .Must(gender => new[] { "Male", "Female", "Other" }.Contains(gender))
               .WithMessage("Gender must be either 'Male', 'Female', or 'Other'.");

            RuleFor(user => user.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is Required.")
                .LessThan(DateTime.Today).WithMessage("Date of Birth must be in the past.");

            RuleFor(user => user.IsActive)
                .NotNull().WithMessage("IsActive must be true or false.");

            RuleFor(user => user.TenantId)
               .NotNull().WithMessage("Tenant ID is Required.");

            RuleFor(user => user.UserRoleId)
               .NotNull().WithMessage("Role ID is Required.");

            RuleFor(user => user.TenancyRoleId)
               .NotNull().WithMessage("Tenancy Role ID is Required.");
        }
    }
}
