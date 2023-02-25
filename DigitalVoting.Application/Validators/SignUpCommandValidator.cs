using DigitalVoting.Application.Commands.SignUp;
using DigitalVoting.Shared.Utils;
using FluentValidation;

namespace DigitalVoting.Application.Validators
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(v => v.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter a username.");
            
            RuleFor(v => v.Username)
                .Must(Validator.ValidUsername)
                .WithMessage("Enter a valid username.");

            RuleFor(v => v.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter a password.");

            RuleFor(v => v.Password)
                .Must(Validator.ValidPassword)
                .WithMessage("Password must contain at least 8 characters, a number, an uppercase letter, a lowercase letter and a special character.");
        }
    }
}