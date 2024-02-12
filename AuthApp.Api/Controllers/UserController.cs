using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers;


public class RespoonseDto
{
    public bool? Status { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
}


[PersianName("کنترلر اول")]
[ApiController]
public class UserController:ControllerBase
{
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [AllowAnonymous]
        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            bool state=false;
            if(state){
                var li=new List<User>{new User{Name="alireza"},new User{Name="akbar"}};
                RespoonseDto response=new RespoonseDto{
                    Message="get list be succcesfull",
                    Data=li,
                    Status=true
                };
                return Ok(response);
            }else{
                throw new Exception("user not found !!!!!!");
                // return NotFound("user not found");
            }
        }

        [AllowAnonymous]
        [HttpGet("Reflection")]
        public IActionResult TestReflection(){
            var controllerType=Assembly.GetExecutingAssembly().GetTypes().Where(type=>typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);
            // var controllerNames = controllerType.Select(type => type.Name.Replace("Controller", ""));
            var controllerNames = controllerType.Select(type =>
            {
                var attribute = type.GetCustomAttribute<PersianNameAttribute>();
                return attribute != null ? attribute.Name : type.Name.Replace("Controller", "");
            });
            return Ok(controllerNames);
        }
}