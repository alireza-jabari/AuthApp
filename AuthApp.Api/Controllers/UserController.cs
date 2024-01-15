using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers;

[ApiController]
public class UserController:ControllerBase
{
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("getusers")]
        public List<User> GetUsers()
        {
            return new List<User>{new User{Name="alireza"},new User{Name="akbar"}};
        }
}