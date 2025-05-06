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
        public DbSet<RideVideosModel> RideVideos { get; set; }
        public DbSet<LikesModel> Likes { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<CoordinatesModel> Coordinates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutesModel>()
                .HasOne(r => r.Creator)
                .WithMany()
                .HasForeignKey(r => r.CreatorId);

            modelBuilder.Entity<GalleryPostModel>()
                .HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId);

            modelBuilder.Entity<RideVideosModel>()
                .HasOne(v => v.Creator)
                .WithMany()
                .HasForeignKey(v => v.CreatorId);

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.Route)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RouteId);

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.GalleryPost)
                .WithMany(g=>g.Likes)
                .HasForeignKey(l => l.GalleryPostId);

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.Video)
                .WithMany(v => v.Likes)
                .HasForeignKey(l => l.VideoId);

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.Comment)
                .WithMany(c => c.Likes)
                .HasForeignKey(l => l.CommentId);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Route)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RouteId);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.GalleryPost)
                .WithMany(g => g.Comments)
                .HasForeignKey(c => c.GalleryPostId);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.Video)
                .WithMany(v => v.Comments)
                .HasForeignKey(c => c.VideoId);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);
        }
    }
}