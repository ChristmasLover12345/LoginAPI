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
        public IActionResult CreateUser([FromBody]UserDTO newUser)
        {
             bool result = _userService.CreateUser(newUser);
    
            if (result)
            {
                return Ok(new { Success = true });
            }
            else
            {
                return BadRequest(new { Success = false, message = "Email already in use." });
            }
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
        public IActionResult UpdatePassword([FromBody] UserDTO user, string guess)
        {


            bool success = _userService.UpdatePassword(user, guess);

            if(success)
            {
                return Ok(new { Success = true });
            }
            else
            {
                return BadRequest(new {Message = "Email not found / Wrong answer"});
            }
            
        }

    }
}