namespace DigitalVoting.Core.Entities;

public partial class Poll
{
    public string Id { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int AmountOfVotes { get; set; }

    public virtual ICollection<VotingOption> VotingOptions { get; } = new List<VotingOption>();
}
