using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersMS.Contracts;
using UsersMS.Infrastructure;

namespace UsersMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/<UsersController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            UserDTO userById = await _usersService.GetUserById(id);

            if (userById.Id < 1)
            {
                return NotFound();
            }

            return Ok(userById);
        }


        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserDTO userToAdd)
        {
            UserDTO createdUser = await _usersService.CreateUserFromDTO(userToAdd);

            if (createdUser.Id < 1)
            {
                return NotFound();
            }

            return Ok(createdUser);
        }

        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            //TODO: 
            // 1. get user by email
            // 2. check user password
            // 3. if password correct generate jwt token
            throw new NotImplementedException();

        }
        
        // POST api/<UsersController>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        // PUT api/<UsersController>
        [HttpPut("editUser")]
        public async Task<IActionResult> EditUser([FromBody] EditUserDTO userToEdit)
        {
            bool isuserEdited = await _usersService.EditUserFromDTO(userToEdit);

            return isuserEdited ? Ok(isuserEdited) : Unauthorized(isuserEdited);
        }

        // PUT api/<UsersController>
        [HttpPut("editPassword")]
        public async Task<IActionResult> EditPassword([FromBody] EditUserPasswordDTO userPasswordToEdit)
        {
            bool isuserEdited = await _usersService.EditUserPasswordFromDTO(userPasswordToEdit);

            return isuserEdited ? Ok(isuserEdited) : Unauthorized(isuserEdited);
        }


    }
}
