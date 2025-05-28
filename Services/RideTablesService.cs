using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Context;
using LoginAPI.Models;
using LoginAPI.Models.DTOS;
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





       public async Task<IEnumerable<GetGalleryDTO>> GetGalleryPosts(int? currentUserId = null, int page = 1, int pageSize = 6)
        {
            return await _dataContext.GalleryPosts
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new GetGalleryDTO
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Caption = p.Title,
                    CreatorName = p.Creator.UserName,
                    ProfilePicture = p.Creator.ProfilePicture,
                    DateCreated = p.DateCreated,
                    LikeCount = p.Likes.Count(l => !l.IsDeleted),
                    CommentCount = p.Comments.Count(c => !c.IsDeleted),
                    IsLikedByCurrentUser = currentUserId.HasValue && currentUserId != 0
                        ? p.Likes.Any(l => l.UserId == currentUserId && !l.IsDeleted)
                        : false
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<GetRoutesDTO>> GetRoutes(int? currentUserId = null, int page = 1, int pageSize = 4)
        {
            return await _dataContext.Routes
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new GetRoutesDTO
                {
                    Id = r.Id,
                    Title = r.RouteName,
                    IsPrivate = r.IsPrivate,
                    CreatorName = r.Creator.UserName,
                    ProfilePicture = r.Creator.ProfilePicture,
                    DateCreated = r.DateCreated,
                    RouteDescription = r.RouteDescription,
                    PathCoordinates = r.PathCoordinates.Select(coord => new CoordinateDTO
                    {
                        Latitude = coord.Latitude,
                        Longitude = coord.Longitude
                    }).ToList(),
                    LikeCount = r.Likes.Count(l => !l.IsDeleted),
                    CommentCount = r.Comments.Count(c => !c.IsDeleted),

                    // Updated logic to treat userId == 0 as guest
                    IsLikedByCurrentUser = currentUserId.HasValue && currentUserId != 0
                        ? r.Likes.Any(l => l.UserId == currentUserId && !l.IsDeleted)
                        : false
                })
                .ToListAsync();
        }






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


        public async Task<bool> RemoveGalleryPostLike(int userId, int galleryPostId)
        {
            var like = await _dataContext.Likes.FirstOrDefaultAsync(like =>
                like.UserId == userId && like.GalleryPostId == galleryPostId && !like.IsDeleted);

            if (like == null) return false;

            like.IsDeleted = true;
            _dataContext.Likes.Update(like);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRouteLike(int userId, int routeId)
        {
            var like = await _dataContext.Likes.FirstOrDefaultAsync(like =>
                like.UserId == userId && like.RouteId == routeId && !like.IsDeleted);

            if (like == null) return false;

            like.IsDeleted = true;
            _dataContext.Likes.Update(like);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveVideoLike(int userId, int videoId)
        {
            var like = await _dataContext.Likes.FirstOrDefaultAsync(like =>
                like.UserId == userId && like.VideoId == videoId && !like.IsDeleted);

            if (like == null) return false;

            like.IsDeleted = true;
            _dataContext.Likes.Update(like);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveCommentLike(int userId, int commentId)
        {
            var like = await _dataContext.Likes.FirstOrDefaultAsync(like =>
                like.UserId == userId && like.CommentId == commentId && !like.IsDeleted);

            if (like == null) return false;

            like.IsDeleted = true;
            _dataContext.Likes.Update(like);
            return await _dataContext.SaveChangesAsync() > 0;
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

        public async Task<List<GetCommentsDTO>> GetCommentsByRouteId(int routeId) => await _dataContext.Comments
        .Include(c => c.User)
        .Where(c => c.RouteId == routeId && !c.IsDeleted)
        .Select(c => new GetCommentsDTO
        {
            CommentText = c.CommentText,
            Username = c.User.UserName,
            ProfilePictureUrl = c.User.ProfilePicture,
            DateCreated = c.CreatedAt
        })
        .ToListAsync();







        public async Task<List<GetCommentsDTO>> GetCommentsByGalleryPostId(int postId) =>
            await _dataContext.Comments
        .Include(c => c.User)
        .Where(c => c.GalleryPostId == postId && !c.IsDeleted)
        .Select(c => new GetCommentsDTO
        {
            CommentText = c.CommentText,
            Username = c.User.UserName,
            ProfilePictureUrl = c.User.ProfilePicture,
            DateCreated = c.CreatedAt
        })
        .ToListAsync();

        public async Task<List<GetCommentsDTO>> GetCommentsByVideoId(int videoId) =>
           await _dataContext.Comments
        .Include(c => c.User)
        .Where(c => c.VideoId == videoId && !c.IsDeleted)
        .Select(c => new GetCommentsDTO
        {
            CommentText = c.CommentText,
            Username = c.User.UserName,
            ProfilePictureUrl = c.User.ProfilePicture,
            DateCreated = c.CreatedAt
        })
        .ToListAsync();

        public async Task<List<GalleryPostModel>> GetUserPosts(int userId) => await _dataContext.GalleryPosts.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).Where(post => post.Creator.UserId == userId).ToListAsync();
        private async Task<UserProfileModel> GetUserByUserName(string userName) => await _dataContext.UserProfile.FirstOrDefaultAsync(user => user.UserName == userName);
        public async Task<List<RoutesModel>> GetUserRoutes(int userId) => await _dataContext.Routes.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).Where(route => route.Creator.UserId == userId).ToListAsync();

        public async Task<List<CoordinatesModel>> GetRouteCoordinates(int routeId) => await _dataContext.Coordinates.Where(coord => coord.RouteId == routeId).ToListAsync();

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

        public async Task<bool> AddRideVideo(RideVideosModel Video)
        {
            Video.CreatedAt = DateTime.UtcNow;
            await _dataContext.RideVideos.AddAsync(Video);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> EditRideVideo(RideVideosModel Video)
        {

            var videoToEdit = await _dataContext.RideVideos.FirstOrDefaultAsync(video => video.Id == Video.Id);

            if (videoToEdit == null) return false;

            videoToEdit.VideoUrl = Video.VideoUrl;
            videoToEdit.Title = Video.Title;
            videoToEdit.IsDeleted = Video.IsDeleted;

            _dataContext.RideVideos.Update(videoToEdit);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<GetRideVideoDTO>> GetRideVideos(int? currentUserId = null, int page = 1, int pageSize = 6)
{
    return await _dataContext.RideVideos
        .Where(v => !v.IsDeleted)
        .OrderByDescending(v => v.CreatedAt)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(v => new GetRideVideoDTO
        {
            Id = v.Id,
            VideoUrl = v.VideoUrl,
            Title = v.Title,
            CreatorName = v.Creator.UserName,
            ProfilePicture = v.Creator.ProfilePicture,
            DateCreated = v.CreatedAt,
            LikeCount = v.Likes.Count(l => !l.IsDeleted),
            CommentCount = v.Comments.Count(c => !c.IsDeleted),
            IsLikedByCurrentUser = currentUserId.HasValue && currentUserId != 0
                ? v.Likes.Any(l => l.UserId == currentUserId && !l.IsDeleted)
                : false
        })
        .ToListAsync();
}

        public async Task<List<RideVideosModel>> GetUserRideVideos(int userId) => await _dataContext.RideVideos.Include(like => like.Likes).Include(com => com.Comments).Include(dad => dad.Creator).Where(video => video.Creator.UserId == userId).ToListAsync();

    }
}