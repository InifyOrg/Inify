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
        private readonly IAccessTokenService _accessTokenService;
        public UsersController(IUsersService usersService, IAccessTokenService accessTokenService)
        {
            _usersService = usersService;
            _accessTokenService = accessTokenService;
        }

        // GET: api/<UsersController>
        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            UserDTO userById = await _usersService.GetUserById(id);

            if (userById.Id < 1)
            {
                return NotFound();
            }

            return Ok(userById);
        }

        // GET: api/<UsersController>
        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            UserDTO userById = await _usersService.GetUserByEmail(email);

            if (userById.Id < 1)
            {
                return NotFound();
            }

            return Ok(userById);
        }
        // POST api/<UsersController>
        [HttpGet("validateLogin/{token}")]
        public async Task<IActionResult> ValidateLogin(string token)
        {
            bool isTokenValid = await _accessTokenService.ValidateAccessToken(token);

            return isTokenValid ? Ok(isTokenValid) : NotFound(isTokenValid);
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
            string accessToken = await _usersService.LoginFromDTO(loginDTO);
            if (accessToken == "")
                return BadRequest("Wrong input data");
            return Ok(accessToken);
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
