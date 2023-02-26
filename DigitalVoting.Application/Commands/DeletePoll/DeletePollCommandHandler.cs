using DigitalVoting.Core.Interfaces;
using MediatR;

namespace DigitalVoting.Application.Commands.DeletePoll
{
    public class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePollCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeletePollCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            int numberOfDeletedOptions = await _unitOfWork.VotingOptions.DeleteByPollAsync(request.PollId);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.Polls.DeleteAsync(request.PollId);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return numberOfDeletedOptions;
        }
    }
}