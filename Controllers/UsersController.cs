using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UsersManagement.Models;
using UsersManagement.Services;

namespace UsersManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //create the  user service 
        private readonly UserService _userService;

        //initialize the _userService within constructor  
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet] //returns all the users /api/users
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        [HttpGet("{id}", Name = "GetUser")]//returns users by Id /api/users/{id}
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]//creates user and return it by Id /api/users
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id}")]//Update the user by replacing it by another /api/users/id
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            //assign the old if to the new object to keep it at the same position
            userIn._id = user._id;
            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id}")]//delete the user by Id /api/users/id
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }

        [HttpPost("search")] //returns all the users matching any of search terms /api/users
        public ActionResult<List<User>> Search(User userSearch)
        {
            var users = _userService.search(userSearch);
            if(users.Count < 1)
            {
                return NotFound();
            }
            return users;
        }
    }
}