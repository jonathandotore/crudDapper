using CrudDapper.Dto;
using CrudDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userInterface.GetAllUsers();

            if (users.Status == false)
                return NotFound(users);
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userInterface.GetUser(id);

            if (user == null)
                return NotFound(user);
            
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto newUser)
        {
            var user = await _userInterface.CreateUser(newUser);

            if (user == null)
                return BadRequest(user);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto userupdateUserDto)
        {
            var user = await _userInterface.UpdateUser(id, userupdateUserDto);

            if (user == null)
                return BadRequest(userupdateUserDto);

            return Ok(user);
        }
    }
}
