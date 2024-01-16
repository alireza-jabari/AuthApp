using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Controllers;

public class UserDto
{
    public string Username { get; set; }="";
    public string Password { get; set; }="";
}


[ApiController]
public class AuthController:ControllerBase
{


    private readonly IConfiguration _configuration;
    public AuthController(IConfiguration configuration)
    {
        _configuration=configuration;   
    }

    [HttpPost("register")]
    public string Register([FromBody] UserDto user)
    {
        string hashedPassword=BCrypt.Net.BCrypt.HashPassword(user.Password);
        return hashedPassword;
    }

    [HttpPost("login")]
    public string Login([FromBody] UserDto user)
    {

        //check user exist

        //validate user pass

        //generate token
        string token=CreateToken(user);
        return token;
    }


    private string CreateToken(UserDto user)
    {
        List<Claim> claims=new List<Claim>{
            new(ClaimTypes.Name,user.Username)
        };
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        var token=new JwtSecurityToken(
            claims:claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials:credentials
        );
        var jwt=new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}