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

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.Route)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RouteId);

            modelBuilder.Entity<LikesModel>()
                .HasOne(l => l.GalleryPost)
                .WithMany()
                .HasForeignKey(l => l.GalleryPostId);

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
        }


    }
}