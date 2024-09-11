using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersMS.Client;
using UsersMS.Contracts;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersMsController : ControllerBase
    {
        private readonly IUsersMsClient _usersMsClient;

        public UsersMsController(IUsersMsClient usersMsClient)
        {
            _usersMsClient = usersMsClient;
        }

        // GET: api/<UsersController>
        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromHeader] string Authorization, long id)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                UserDTO userById = await _usersMsClient.GetUserByID(id);

                if (userById.Id < 1)
                {
                    return NotFound();
                }

                return Ok(userById);
            }
            return Unauthorized();
        }

        // GET: api/<UsersController>
        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromHeader] string Authorization, string email)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                UserDTO userById = await _usersMsClient.GetUserByEmail(email);

                if (userById.Id < 1)
                {
                    return NotFound();
                }

                return Ok(userById);
            }
            return Unauthorized();

        }
        // POST api/<UsersController>
        [HttpGet("validateLogin/{token}")]
        public async Task<IActionResult> ValidateLogin(string token)
        {
            bool isTokenValid = await _usersMsClient.ValidateAccessToken(token);

            return isTokenValid ? Ok(isTokenValid) : NotFound(isTokenValid);
        }

        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserDTO userToAdd)
        {
            UserDTO createdUser = await _usersMsClient.Register(userToAdd);

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
            string accessToken = await _usersMsClient.Login(loginDTO);
            if (accessToken == "")
                return BadRequest("Wrong input data");
            return Ok(accessToken);
        }

        // PUT api/<UsersController>
        [HttpPut("editUser")]
        public async Task<IActionResult> EditUser([FromHeader] string Authorization, [FromBody] EditUserDTO userToEdit)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                bool isuserEdited = await _usersMsClient.EditUser(userToEdit);

                return isuserEdited ? Ok(isuserEdited) : Unauthorized(isuserEdited);
            }
            return Unauthorized();
        }

        // PUT api/<UsersController>
        [HttpPut("editPassword")]
        public async Task<IActionResult> EditPassword([FromHeader] string Authorization, [FromBody] EditUserPasswordDTO userPasswordToEdit)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                bool isuserEdited = await _usersMsClient.EditUserPassword(userPasswordToEdit);

                return isuserEdited ? Ok(isuserEdited) : Unauthorized(isuserEdited);

            }
            return Unauthorized();
        }


    }
}
