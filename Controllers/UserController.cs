using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using LoginAPI.Models.DTOS;
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
            UserCreateDTO result = _userService.CreateUser(newUser);
    
            if (result != null)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest(new { Success = false, message = "Email already in use." });
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);

            if (user != null)
            {
                return Ok(new {user});
            }
            else
            {
                return Unauthorized(new {Message = "No user found linked to this email"});
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            UserCreateDTO result = _userService.Login(user);

            if (result != null && result.Token != null)
            {
                return Ok(new {result });
            }
            else
            {
                 return Unauthorized(new { Message = "Login was unsuccessful. Invalid Email or Password" });
            }
        }

        [HttpGet]
        [Route("AuthenticUser")]
        [Authorize]
        public string AuthenticUserCheck()
        {
            return "You ARE supposed to be here!";
        }
        

        [HttpPut]
        [Route("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] UserDTO user)
        {

            
            bool success = _userService.UpdatePassword(user);

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