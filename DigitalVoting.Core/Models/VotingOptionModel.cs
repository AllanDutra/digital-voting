namespace DigitalVoting.Core.Models
{
    public class VotingOptionModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int? AmountOfVotes { get; set; }
    }
}