using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }

            public string? Password { get; set; }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            // Step 1: validate the username/password
            var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Step 2: create a token. We need to pass the Secret ass a Byte Array so we use Encoding.ASCII.GetBytes
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(this.configuration["Authentication:SecretForKey"]));
            var signingCredentilas = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            // The claims: sub is a standaredised key for the unique user identifier. We can give any name but this is preffered
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                this.configuration["Authentication:Issuer"],
                this.configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentilas);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private CityInfoUser ValidateUserCredentials(string? userName, string? password)
        {
            /* We dont't have a user DB or table. If we have, check the passed-through username/password 
             * against what's stored in the database.

             For demo perposes, we assume the crenedentials are valid.
            
             return a new CityInfoUser (vakues would norally come from your user DB/table)*/
            return new CityInfoUser(
                1,
                userName ?? "",
                "Simeon",
                "Yordanov",
                "Sofia");
        }

        public class CityInfoUser
        {
            public CityInfoUser(
                int userId, 
                string userName, 
                string firstName, 
                string lastName, 
                string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }

            public int UserId { get; set; } 

            public string UserName { get; set; } = null!;

            public string FirstName { get; set; } = null!;

            public string LastName { get; set; } = null!;

            public string City { get; set; } = null!;
        }
    }
}
