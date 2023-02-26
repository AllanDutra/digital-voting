using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DigitalVoting.Infrastructure.Persistence.Services
{
    public class TokenService : ITokenService
    {
        private const int ACCESS_TOKEN_DURATION_IN_HOURS = 1;
        private readonly IConfiguration _configuration;
        private static JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private byte[] GetPrivateKey()
        {
            return Encoding.ASCII.GetBytes(_configuration.GetSection("SecretKey").Value);
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidateAudience = false;
            validationParameters.ValidateIssuer = false;

            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(GetPrivateKey());

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

        public string GenerateVoterAccessToken(string voterUsername)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = (DateTime.Now.AddHours(ACCESS_TOKEN_DURATION_IN_HOURS).ToUniversalTime()),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GetPrivateKey()), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("Username", voterUsername)
                }),
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            var tokenWrite = _tokenHandler.WriteToken(token);

            return tokenWrite;
        }

        public Voter ReadToken(string jwtToken)
        {
            Voter tokenDTO = new Voter();

            var Claims = ValidateToken(jwtToken).Claims;

            tokenDTO.Username = Claims.FirstOrDefault(p => p.Type == "Username").Value;

            return tokenDTO;
        }
    }
}