using FluentValidation;
using HRMS.Dtos.User.Login.LoginRequestDtos;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Utility.Validators.User.Login
{
    public class UserLoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(login => login.SubdomainName)
              .NotEmpty().WithMessage("Subdomain Name is Required.")
              .Length(2, 50).WithMessage("Subdomain Name must be between 2 and 50 characters.");

            RuleFor(login => login.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or Email is Required.")
                .Length(5, 100).WithMessage("Username or Email must be between 5 and 100 characters.")
                .Must(IsValidEmailOrUserName).WithMessage("Invalid Email or Username format.");

            RuleFor(login => login.Password)
                .NotEmpty().WithMessage("Password is Required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
        private bool IsValidEmailOrUserName(string input)
        {
            return IsValidEmail(input) || IsValidUserName(input);
        }

        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private bool IsValidUserName(string userName)
        {
            return userName.Length >= 2 && userName.Length <= 50;
        }

    }
}
