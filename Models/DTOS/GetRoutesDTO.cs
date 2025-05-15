using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models.DTOS
{
  public class GetRoutesDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsPrivate { get; set; }
    public string CreatorName { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime DateCreated { get; set; }
    public string? RouteDescription { get; set; }

    public List<CoordinateDTO> PathCoordinates { get; set; } = new();
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }

    public bool IsLikedByCurrentUser { get; set; }
}

    public class CoordinateDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}