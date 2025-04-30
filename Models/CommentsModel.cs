using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int? RouteId { get; set; }
        public virtual RoutesModel? Route { get; set; }

        public int? GalleryPostId { get; set; }
        public virtual GalleryPostModel? GalleryPost { get; set; }


        public string? CommentText { get; set; }
        public string? CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public virtual List<LikesModel>? Likes { get; set; }
    
    }
}