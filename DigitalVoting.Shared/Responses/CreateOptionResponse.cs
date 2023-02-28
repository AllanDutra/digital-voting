namespace DigitalVoting.Shared.Responses
{
    public class CreateOptionResponse
    {
        public CreateOptionResponse(string message, Guid votingOptionId)
        {
            Message = message;
            VotingOptionId = votingOptionId;
        }

        public string Message { get; set; }
        public Guid VotingOptionId { get; set; }
    }
}