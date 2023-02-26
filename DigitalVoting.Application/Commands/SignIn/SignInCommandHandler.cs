using System.Net;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;
using DigitalVoting.Core.Interfaces.Services;
using DigitalVoting.Shared.Exceptions;
using DigitalVoting.Shared.Utils;
using MediatR;

namespace DigitalVoting.Application.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly IVoterRepository _voterRepository;
        private readonly ITokenService _tokenService;
        public SignInCommandHandler(IVoterRepository voterRepository, ITokenService tokenService)
        {
            _voterRepository = voterRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            Voter searchedVoter = new Voter { Username = request.Username, Password = Cryptography.EncryptToSha256(request.Password) };

            Voter voter = await _voterRepository.CheckIfVoterExistsAsync(searchedVoter);

            if (voter == null)
                throw new ResponseException("Incorrect username or password.", HttpStatusCode.BadRequest);

            var token = _tokenService.GenerateVoterAccessToken(voter.Username);

            return token;
        }
    }
}