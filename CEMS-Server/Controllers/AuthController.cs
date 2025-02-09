using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CemsContext _context;
    private readonly IConfiguration _config;

    public AuthController(CemsContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO model)
    {

        var user = _context.CemsUsers.FirstOrDefault(u => u.UsrEmployeeId == model.Username);


    if (user == null)
        return NotFound("Invalid username");

        if(!BCrypt.Net.BCrypt.Verify(model.Password, user.UsrPassword)){
            return Unauthorized("Invalid password");
        }
        
        var validateUser  = new LoginDTO { Username = user.UsrEmployeeId, Password = user.UsrPassword };

        var token = GenerateJwtToken(validateUser);
        return Ok(new { token });
    }

    private string GenerateJwtToken(LoginDTO user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new[] { new Claim(ClaimTypes.Name, user.Username) },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
