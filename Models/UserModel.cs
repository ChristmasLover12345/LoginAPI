using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Creator { get; set; }
        public string? Email { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public string? Question {get; set;}
        public string? answerSalt {get; set;}
        public string? answerHash {get; set;}
        
        
    }
}