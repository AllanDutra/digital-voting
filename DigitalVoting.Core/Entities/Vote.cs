namespace DigitalVoting.Core.Entities;

public partial class Vote
{
    public string VoterUsername { get; set; }

    public Guid VotingOptionId { get; set; }

    public virtual Voter VoterUsernameNavigation { get; set; }

    public virtual VotingOption VotingOption { get; set; }
}
