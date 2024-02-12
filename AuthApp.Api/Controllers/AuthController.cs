using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Controllers;

public class UserDto
{
    public string Username { get; set; }="";
    public string Password { get; set; }="";
}


[PersianName("کنترلر سوم")]
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

        //todo check user exist

        //todo validate user pass

        //todo generate access token
        string token=CreateToken(user);

        //todo generate refresh token

        //todo save in db

        //todo : send refresh token in http only cookie and access token in reponse to client
        return token;
    }


    private string CreateToken(UserDto user)
    {
        System.Console.WriteLine(DateTime.Now);
        System.Console.WriteLine(DateTime.Now.AddSeconds(10));
        List<Claim> claims=new List<Claim>{
            new(ClaimTypes.Name,user.Username)
        };
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        var token=new JwtSecurityToken(
            claims:claims,
            expires:DateTime.Now.AddSeconds(10),
            signingCredentials:credentials,
            issuer:"alireza-jabari",
            audience:"alireza-jabari"
        );
        var jwt=new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }



    [Authorize]
    [HttpGet("getData")]
    public string GetData()
    {
        Console.WriteLine(DateTime.Now);
        return "data";
    }



    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var jwtTokenHandler=new JwtSecurityTokenHandler();
        // jwtTokenHandler.ValidateToken();
        return Ok();
    }
}