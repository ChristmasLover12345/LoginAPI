using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        
        private readonly UserServices _userService;
        public UserController(UserServices userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public bool CreateUser([FromBody]UserDTO newUser)
        {
            return _userService.CreateUser(newUser);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserDTO user)
        {
            string stringToken = _userService.Login(user);

            if (stringToken != null)
            {
                return Ok(new { Token = stringToken });
            }
            else
            {
                 return Unauthorized(new { Message = "Login was unsuccessful. Invalid Email or Password" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AuthenticUser")]
        public string AuthenticUserCheck()
        {
            return "You ARE supposed to be here!";
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public bool UpdatePassword([FromBody] UserDTO user)
        {
            return _userService.UpdatePassword(user);
        }

    }
}