namespace DigitalVoting.Core.Entities;

public partial class VotingOption
{
    public string Id { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? AmountOfVotes { get; set; }

    public string PollId { get; set; } = null!;

    public virtual Poll Poll { get; set; } = null!;
}
