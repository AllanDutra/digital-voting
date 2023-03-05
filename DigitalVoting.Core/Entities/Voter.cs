namespace DigitalVoting.Core.Entities;

public partial class Voter
{
    public string Username { get; set; }

    public string Password { get; set; }

    public virtual ICollection<VoterVotingOption> VoterVotingOptions { get; } = new List<VoterVotingOption>();
}
