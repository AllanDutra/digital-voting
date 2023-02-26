using DigitalVoting.Application.Commands.CreateNewPoll;
using FluentValidation;

namespace DigitalVoting.Application.Validators
{
    public class CreateNewPollCommandValidator : AbstractValidator<CreateNewPollCommand>
    {
        public CreateNewPollCommandValidator()
        {
            RuleFor(p => p.PollDescription)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter a value to poll description.");

            RuleFor(p => p.PollVotingOptions)
                .NotEmpty()
                .NotNull()
                .Must(ValidPollOptions)
                .WithMessage("Enter valid voting options.");

            RuleFor(p => p.PollVotingOptions)
                .Must(ValidPollOptionsLength)
                .WithMessage("Enter at least two voting options.");
        }

        private static bool ValidPollOptions(List<string> pollVotingOptions)
        {
            bool areValid = true;

            pollVotingOptions.ForEach(po =>
            {
                if (po == null)
                    areValid = false;
                else if (po.Trim() == "")
                    areValid = false;
            });

            return areValid;
        }

        private static bool ValidPollOptionsLength(List<string> pollVotingOptions)
        {
            if (pollVotingOptions.Count < 2)
                return false;

            return true;
        }
    }
}