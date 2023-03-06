using DigitalVoting.Core.Models;
using MediatR;

namespace DigitalVoting.Application.Queries.GetAllPolls
{
    public class GetAllPollsQuery : IRequest<List<PollModel>>
    {

    }
}