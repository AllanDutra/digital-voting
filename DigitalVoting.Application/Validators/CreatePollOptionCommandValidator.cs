using DigitalVoting.Application.Commands.CreatePollOption;
using FluentValidation;

namespace DigitalVoting.Application.Validators
{
    public class CreatePollOptionCommandValidator : AbstractValidator<CreatePollOptionCommand>
    {
        public CreatePollOptionCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a description.");

            RuleFor(p => p.PollId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a poll id.");
        }
    }
}