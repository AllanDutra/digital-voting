namespace DigitalVoting.Shared.Responses
{
    public class CreateNewPollResponse
    {
        public CreateNewPollResponse(string message, Guid pollId)
        {
            Message = message;
            PollId = pollId;
        }

        public string Message { get; set; }
        public Guid PollId { get; set; }
    }
}