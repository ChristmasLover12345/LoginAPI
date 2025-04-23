using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LoginAPI.Models.CoordinatesModel;


namespace LoginAPI.Models
{
    public class RoutesModel
    {


        public int Id { get; set; }
        public int CreatorId { get; set; }


        public string? RouteName { get; set; }
        public string? RouteDescription { get; set; }
        public string? ImageUrl { get; set; }

        public string? CityName { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }

        virtual public List<CoordinatesModel>? PathCoordinates { get; set; }
        public List<LikesModel>? Likes { get; set; }
        public List<CommentsModel>? Comments { get; set; }


    }
}