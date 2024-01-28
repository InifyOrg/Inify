using Microsoft.AspNetCore.Mvc;
using UsersMS.Contracts;

namespace UsersMS.Host.Controllers
{
    public class UsersController : Controller
    {
        // GET: api/<UsersController>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            
            throw new NotImplementedException();
            //UserDTO userById = await _userService.GetUserById(id);

            //if(userById == null)
            //{
            //    return NotFound();
            //}

            //return Ok(userById);
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserDTO userToAdd)
        {
            throw new NotImplementedException();

            //return await _userService.CreateUserFromDTO(userToAdd);
        }

        // PUT api/<UsersController>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditUserDTO userToEdit)
        {
            throw new NotImplementedException();

            //return await _userService.EditUserFromDTO(userToEdit);
        }

    }
}
