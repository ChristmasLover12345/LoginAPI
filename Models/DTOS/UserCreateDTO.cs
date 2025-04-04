using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models.DTOS
{
    public class UserCreateDTO
    {
        public string? Token { get; set; } 
        public int Id { get; set; }
    }
}