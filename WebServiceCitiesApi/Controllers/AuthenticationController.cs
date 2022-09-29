using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/authentication")]

    public class AuthenticationController : ControllerBase
    {
        //do not forget to add app.UseAuthentication(); in Program.cs
        // glöm inte lägga till [Authorize] i Controlleers som måste använda detta

        //OAuth2 och OpenID Connect är förbättringar som kan användas för Aukt och Auth


        private IConfiguration _config;//for reading from appsettingsDevelopement and so on
        //for usage in the requestbody
        public AuthenticationController(IConfiguration config) { _config = config ?? throw new ArgumentNullException("blalalala"); }


        public  class AuthenticationRequestBody
        {
            
            public AuthenticationRequestBody(string firstname, string lastname)
            { FirstName = firstname; LastName = lastname; }

            public string? UserName { get; set; }
            public string? Password { get; set; }

            public string? FirstName { get; set; }
            public string? LastName { get; set; }

        }// class AuthenticationRequestBody

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequestBody request)
        {
            //kan lagras i appsettings.Development under utveckling
            // annars på samma sätt som med connectionstrings, AppVault eller i SystemSettings på burken
            //på jwt.io kan man decoda strängen som används som token
            var t = ValidateUserCredentials(request.UserName, request.Password);
            if(t is null) return Unauthorized();

            //skapa en token
            var securitykey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var signingcredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("sub", request.UserName)); //sub är en standardiserad nyckel för userid i plattformen
            claims.Add(new Claim("given_name", t.FirstName));
            claims.Add(new Claim("family_name", t.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.UtcNow, DateTime.UtcNow.AddHours(1), //valid 1 hour
                signingcredentials
                );

            //denna token används sedan av klienten vid anrop till övriga metoder
            //NuGet Microsoft.aspnetcore.authentication.jwtbearer
            // se Program.cs och builder.Services.AddAuthentication().AddJwtBearer()
            return Ok(jwtSecurityToken);

        }

        private AuthenticationRequestBody ValidateUserCredentials(string? uname,string? pswd)
        {
            //authenticera!
            return new AuthenticationRequestBody("förstaname","efternamnetsson");


        }
    }
}
