using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class UserDTO
    {
        public string? Email { get; set;}
        public string? Password { get; set;}
        public string? Question { get; set;}
        public string? Answer { get; set; }
        
    }

}

