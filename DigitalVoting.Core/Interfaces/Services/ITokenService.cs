using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateVoterAccessToken(string voterUsername);

        Voter ReadToken(string jwtToken);
    }
}