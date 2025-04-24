using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class LikesModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? RouteId { get; set; }
        public int? GalleryPostId { get; set; }
        public int? CommentId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}