using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class RideVideosModel
    {
        public int Id { get; set; }
        public virtual UserProfileModel? Creator { get; set; }
        public int CreatorId { get; set; }
        public string? VideoUrl { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<LikesModel>? Likes { get; set; }
        public virtual ICollection<CommentsModel>? Comments { get; set; }

    }
}