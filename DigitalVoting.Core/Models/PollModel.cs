namespace DigitalVoting.Core.Models
{
    public class PollModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int AmountOfVotes { get; set; }

        public List<VotingOptionModel> VotingOptions { get; set; }
    }
}