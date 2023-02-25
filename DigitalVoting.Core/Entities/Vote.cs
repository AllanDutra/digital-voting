namespace DigitalVoting.Core.Entities;

public partial class Vote
{
    public string VoterUsername { get; set; } = null!;

    public string VotingOptionId { get; set; } = null!;

    public virtual Voter VoterUsernameNavigation { get; set; } = null!;

    public virtual VotingOption VotingOption { get; set; } = null!;
}
