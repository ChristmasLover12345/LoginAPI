using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models.DTOS
{
    public class GetCommentsDTO
    {
    public string CommentText { get; set; }
    public string Username { get; set; }
    public string ProfilePictureUrl { get; set; }
    public DateTime DateCreated { get; set; }

    }
}