using Microsoft.AspNetCore.Mvc;
using UsersMS.Contracts;
using UsersMS.Infrastructure;

namespace UsersMS.Host.Controllers
{
    public class UsersController : Controller
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

            if (userById == null)
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

            if (createdUser == null)
            {
                return NotFound();
            }

            return Ok(createdUser);

        }

        // PUT api/<UsersController>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditUserDTO userToEdit)
        {
            UserDTO editedUser = await _usersService.EditUserFromDTO(userToEdit);

            if (editedUser == null)
            {
                return NotFound();
            }

            return Ok(editedUser);

        }

    }
}
