namespace DigitalVoting.Shared.Responses
{
    public class SignInResponse
    {
        public SignInResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}