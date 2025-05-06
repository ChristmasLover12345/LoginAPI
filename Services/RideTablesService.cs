using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Context;
using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;



namespace LoginAPI.Services
{
    public class RideTablesService
    {

        private readonly DataContext _dataContext;
        public RideTablesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }





        public async Task<List<GalleryPostModel>> GetGalleryPosts() => await _dataContext.GalleryPosts.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).ToListAsync();

        public async Task<IEnumerable<RoutesModel>> GetRoutes() => await _dataContext.Routes.Include(cord => cord.PathCoordinates).Include(like => like.Likes).Include(com => com.Comments).ThenInclude(comment => comment.User).Include(dad => dad.Creator).ToListAsync();

        public async Task<bool> AddGalleryPost(GalleryPostModel post)
        {
            post.DateCreated = DateTime.UtcNow;
            await _dataContext.GalleryPosts.AddAsync(post);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> RemoveGalleryPost(int id)
        {
            var post = await _dataContext.GalleryPosts.FirstOrDefaultAsync(post => post.Id == id);

            if (post == null) return false;

            post.IsDeleted = true;

            _dataContext.GalleryPosts.Update(post);
            return await _dataContext.SaveChangesAsync() != 0;

        }

        public async Task<bool> AddRoute(RoutesModel route)
        {
            route.DateCreated = DateTime.UtcNow;
            await _dataContext.Routes.AddAsync(route);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> PrivateRoute(int id)
        {
            var route = await _dataContext.Routes.FirstOrDefaultAsync(route => route.Id == id);


            if (route == null)
            {
                return false;
            }

            if (route.IsPrivate == true)
            {
                route.IsPrivate = false;
            }
            else
            {
                route.IsPrivate = true;
            }

            _dataContext.Routes.Update(route);
            return await _dataContext.SaveChangesAsync() != 0;
        }


        public async Task<bool> RemoveRoute(int id)
        {
            var route = await _dataContext.Routes.FirstOrDefaultAsync(route => route.Id == id);

            if (route == null) return false;

            route.IsDeleted = true;

            _dataContext.Routes.Update(route);
            return await _dataContext.SaveChangesAsync() != 0;

        }


        public async Task<bool> AddLike(LikesModel like)
        {
            like.CreatedAt = DateTime.UtcNow;
            await _dataContext.Likes.AddAsync(like);
            return await _dataContext.SaveChangesAsync() != 0;
        }
        public async Task<bool> AddComment(CommentsModel comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            await _dataContext.Comments.AddAsync(comment);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> EditGalleryPost(GalleryPostModel post)
        {
            var postToEdit = await GetGalleryPostById(post.Id);


            if (postToEdit == null) return false;

            postToEdit.Title = post.Title;
            postToEdit.ImageUrl = post.ImageUrl;
            postToEdit.Description = post.Description;
            postToEdit.Comments = post.Comments;
            postToEdit.DateCreated = post.DateCreated;
            postToEdit.IsDeleted = post.IsDeleted;
            postToEdit.Likes = post.Likes;

            _dataContext.GalleryPosts.Update(postToEdit);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private async Task<GalleryPostModel> GetGalleryPostById(int Id) => await _dataContext.GalleryPosts.FindAsync(Id);
       

        

       public async Task<List<LikesModel>> GetLikesById(int Id) => await _dataContext.Likes.Where(post => post.UserId == Id && post.IsDeleted == false).ToListAsync();


        public async Task<bool> RemoveLike(int userId, int TargetId)
        {
            var like = await _dataContext.Likes.FirstOrDefaultAsync(like => like.UserId == userId && (like.GalleryPostId == TargetId || like.UserId == userId && like.RouteId == TargetId || like.UserId == userId && like.CommentId == TargetId));

            if (like == null)
            {
                return false;
            }

            like.IsDeleted = true;

            _dataContext.Likes.Update(like);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> RemoveComment(int commentId, int userId)
        {
            var comment = await _dataContext.Comments.FirstOrDefaultAsync(coment => coment.Id == commentId && coment.UserId == userId);

            if (comment == null)
            {
                return false; // Comment not found or does not belong to the user
            }

            comment.IsDeleted = true;

            _dataContext.Comments.Update(comment);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<List<GalleryPostModel>> GetUserPosts(int userId) => await _dataContext.GalleryPosts.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).Where(post => post.Creator.UserId == userId).ToListAsync();
        private async Task<UserProfileModel> GetUserByUserName(string userName) => await _dataContext.UserProfile.FirstOrDefaultAsync(user => user.UserName == userName);
        public async Task<List<RoutesModel>> GetUserRoutes(int userId) => await _dataContext.Routes.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).Where(route => route.Creator.UserId == userId).ToListAsync();

       
        public async Task<bool> AddUserProfile(UserProfileModel profile)
        {

            var user = await GetUserByUserName(profile.UserName);

            UserProfileModel newProfile = new();


            if (user != null)
            {
                return false;

            }
            else
            {

                newProfile.UserName = profile.UserName;
                newProfile.UserId = profile.UserId;
                newProfile.Name = profile.Name;
                newProfile.Location = profile.Location;
                newProfile.BikeType = profile.BikeType;
                newProfile.RidingExperience = profile.RidingExperience;
                newProfile.RidingPreference = profile.RidingPreference;
                newProfile.RideConsistency = profile.RideConsistency;
                newProfile.ProfilePicture = profile.ProfilePicture;

                await _dataContext.UserProfile.AddAsync(newProfile);
                return await _dataContext.SaveChangesAsync() != 0;
            }

        }

        public async Task<bool> EditUserProfile(UserProfileModel profile)
        {
            var profileToEdit = await GetProfileById(profile.Id);


            if (profileToEdit == null) return false;

            profileToEdit.BikeType = profile.BikeType;
            profileToEdit.UserId = profile.UserId;
            profileToEdit.Location = profile.Location;
            profileToEdit.Name = profile.Name;
            profileToEdit.ProfilePicture = profile.ProfilePicture;
            profileToEdit.RideConsistency = profile.RideConsistency;
            profileToEdit.RidingExperience = profile.RidingExperience;
            profileToEdit.RidingPreference = profile.RidingPreference;


            _dataContext.UserProfile.Update(profileToEdit);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<UserProfileModel> GetProfileById(int profileId) => await _dataContext.UserProfile.FirstOrDefaultAsync(profile => profile.UserId == profileId);

    }
}