using System.Net;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces;
using DigitalVoting.Shared.Exceptions;
using DigitalVoting.Shared.Utils;
using MediatR;

namespace DigitalVoting.Application.Commands.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly IVoterRepository _voterRepository;
        public SignUpCommandHandler(IVoterRepository voterRepository)
        {
            _voterRepository = voterRepository;
        }

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            bool usernameAlreadyExists = await _voterRepository.CheckIfUsernameAlreadyExistsAsync(request.Username);

            if (usernameAlreadyExists)
                throw new ResponseException("The username entered is already in use.", HttpStatusCode.BadRequest);

            var encryptedPassword = Cryptography.EncryptToSha256(request.Password);

            var voter = new Voter { Username = request.Username, Password = encryptedPassword };

            await _voterRepository.SignUpAsync(voter);

            return Unit.Value;
        }
    }
}