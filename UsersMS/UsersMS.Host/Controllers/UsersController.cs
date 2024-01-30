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
        public async Task<IActionResult> Get(long id)
        {
            UserDTO userById = await _usersService.GetUserById(id);

            if (userById.Id < 1)
            {
                return NotFound();
            }

            return Ok(userById);
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserDTO userToAdd)
        {
            UserDTO createdUser = await _usersService.CreateUserFromDTO(userToAdd);

            if (createdUser.Id < 1)
            {
                return NotFound();
            }

            return Ok(createdUser);
        }

        // PUT api/<UsersController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditUserDTO userToEdit)
        {
            bool isuserEdited = await _usersService.EditUserFromDTO(userToEdit);

            return Ok(isuserEdited);
        }


    }
}
