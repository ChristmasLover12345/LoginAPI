using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class GalleryPostModel
    {
        
      

        public string? UserName { get; set; }
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        virtual public List<LikesModel>? Likes { get; set; }
        virtual public List<CommentsModel>? Comments { get; set; }
    }
}