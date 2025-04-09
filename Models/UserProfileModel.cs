using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? BikeType { get; set; }
        public string? RidingExperience { get; set; }
        public string? RidingPreference { get; set; }
        public string? RideConsistency  { get; set; }
        public string? ProfilePicture { get; set; }
    }
}