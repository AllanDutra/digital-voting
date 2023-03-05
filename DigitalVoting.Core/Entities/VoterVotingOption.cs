namespace DigitalVoting.Core.Entities
{
    public class VoterVotingOption
    {
        public Guid Id { get; set; }

        public string VoterUsername { get; set; }

        public Guid VotingOptionId { get; set; }

        public Guid PollId { get; set; }

        public virtual Voter VoterUsernameNavigation { get; set; }

        public virtual VotingOption VotingOption { get; set; }
    }
}