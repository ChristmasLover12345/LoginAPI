using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace LoginAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
         public DbSet<UserProfileModel> UserProfile { get; set; }
        public DbSet<GalleryPostModel> GalleryPosts { get; set; }
        public DbSet<RoutesModel> Routes { get; set; }
        public DbSet<LikesModel> Likes {get; set;}
        public DbSet<CommentsModel> Comments { get; set;}

    }
}