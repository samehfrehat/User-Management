using System.Collections.Generic;
using UsersManagement.Models;
using UsersManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace UsersManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }


        
    }
}