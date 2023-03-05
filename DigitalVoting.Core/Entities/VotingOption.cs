namespace DigitalVoting.Core.Entities;

public partial class VotingOption
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public int? AmountOfVotes { get; set; }

    public Guid PollId { get; set; }

    public virtual Poll Poll { get; set; }

    public virtual ICollection<VoterVotingOption> VoterVotingOptions { get; } = new List<VoterVotingOption>();
}
