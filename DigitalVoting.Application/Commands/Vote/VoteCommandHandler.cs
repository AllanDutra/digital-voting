using System.Net;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces;
using DigitalVoting.Core.Interfaces.Services;
using DigitalVoting.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalVoting.Application.Commands.VoteCommand
{
    public class VoteCommandHandler : IRequestHandler<VoteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        public VoteCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        private string GetJwtToken()
        {
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            return authorizationHeader.Replace("Bearer ", "");
        }

        public async Task<Unit> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            string Voter_Username = _tokenService.ReadToken(GetJwtToken()).Username;

            Guid pollId = await _unitOfWork.VotingOptions.GetPollIdAsync(request.VotingOption_Id);

            bool userAlreadyVotedInThisPoll = await _unitOfWork.Polls.CheckIfUserAlreadyVotedInPollAsync(Voter_Username, pollId);

            if (userAlreadyVotedInThisPoll)
                throw new ResponseException("You have already voted in this poll.", HttpStatusCode.BadRequest);

            VoterVotingOption vote = new VoterVotingOption
            {
                Id = Guid.NewGuid(),
                PollId = pollId,
                VoterUsername = Voter_Username,
                VotingOptionId = request.VotingOption_Id
            };

            await _unitOfWork.VotersVotingOptions.AddAsync(vote);

            await _unitOfWork.CompleteAsync();

            int amountOfVotesInVotingOption = await _unitOfWork.VotingOptions.GetAmountOfVotesAsync(request.VotingOption_Id);

            await _unitOfWork.VotingOptions.UpdateAmountOfVotesAsync(request.VotingOption_Id, amountOfVotesInVotingOption + 1);

            await _unitOfWork.CompleteAsync();

            int amountOfVotesInPoll = await _unitOfWork.Polls.GetAmountOfVotesAsync(pollId);

            await _unitOfWork.Polls.UpdateAmountOfVotesAsync(pollId, amountOfVotesInPoll + 1);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}