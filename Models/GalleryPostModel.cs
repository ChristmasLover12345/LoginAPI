using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class GalleryPostModel
    { 
        public int Id { get; set; }
        public virtual UserProfileModel? Creator { get; set; }
        public int CreatorId { get; set; }

        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<LikesModel>? Likes { get; set; }
        public virtual ICollection<CommentsModel>? Comments { get; set; }
    }
}