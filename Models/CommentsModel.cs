using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int? RouteId { get; set; }
        [JsonIgnore]
        public virtual RoutesModel? Route { get; set; }

        public int? GalleryPostId { get; set; }
        [JsonIgnore]
        public virtual GalleryPostModel? GalleryPost { get; set; }


        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<LikesModel>? Likes { get; set; }

        [JsonIgnore]
        public virtual UserProfileModel? User { get; set; }

    }
}