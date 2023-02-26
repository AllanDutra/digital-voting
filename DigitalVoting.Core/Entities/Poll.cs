namespace DigitalVoting.Core.Entities;

public partial class Poll
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public int AmountOfVotes { get; set; }

    public virtual ICollection<VotingOption> VotingOptions { get; } = new List<VotingOption>();
}
