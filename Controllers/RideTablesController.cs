using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;





namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class RideTablesController : ControllerBase
    {

        private readonly RideTablesService _rideTablesService;
        public RideTablesController(RideTablesService rideTablesService)
        {
            _rideTablesService = rideTablesService;
        }

        [HttpGet("GetGallery")]
        public async Task<IActionResult> GetAllGalleryPosts()
        {

            var posts = await _rideTablesService.GetGalleryPosts();

            if (posts != null) return Ok(posts);

            return BadRequest(new { Message = "The gallery is empty.." });

        }

        [HttpGet("GetUserPosts/{userId}")]
        public async Task<IActionResult> GetUserPosts(int userId)
        {

            var posts = await _rideTablesService.GetUserPosts(userId);

            if (posts != null) return Ok(posts);

            return BadRequest(new { Message = "This user's gallery is empty.." });

        }

        [HttpPost("EditPost")]
        public async Task<IActionResult> EditGalleryPost([FromBody] GalleryPostModel post)
        {
            var success = await _rideTablesService.EditGalleryPost(post);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "No post found" });
        }

        [HttpPost("AddGalleryPost")]
        public async Task<IActionResult> AddGalleryPost([FromBody] GalleryPostModel post)
        {

            var success = await _rideTablesService.AddGalleryPost(post);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "Post creation was not successful" });

        }

        [HttpDelete("RemoveGalleryPost/{id}")]
        public async Task<IActionResult> RemoveGalleryPost(int id)
        {
            var success = await _rideTablesService.RemoveGalleryPost(id);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no post to be removed was found" });

        }

        [HttpGet("GetRoutes/{userId}")]
        public async Task<IActionResult> GetAllRoutes(int userId)
        {
            var routes = await _rideTablesService.GetRoutes(userId);

            if (routes == null || !routes.Any())
            {
                return NotFound(new { Message = "No routes found." });
            }

            return Ok(routes);
        }


        [HttpGet("GetUserRoutes/{userId}")]
        public async Task<IActionResult> GetUserRoutes(int userId)
        {

            var routes = await _rideTablesService.GetUserRoutes(userId);

            if (routes != null) return Ok(routes);

            return BadRequest(new { Message = "This user has no routes" });

        }



        [HttpPost("AddRoute")]
        public async Task<IActionResult> AddRoute([FromBody] RoutesModel route)
        {
            var success = await _rideTablesService.AddRoute(route);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "Route creation was not successful" });
        }

        [HttpPost("PrivateRoute/{id}")]
        public async Task<IActionResult> PrivateRoute(int id)
        {
            var success = await _rideTablesService.PrivateRoute(id);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "No route found" });
        }

        [HttpDelete("RemoveRoute/{id}")]
        public async Task<IActionResult> RemoveLike(int id)
        {
            var success = await _rideTablesService.RemoveRoute(id);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no route to be removed was found" });

        }

        [HttpGet("GetVideos")]
        public async Task<IActionResult> GetAllVideos()
        {

            var videos = await _rideTablesService.GetRideVideos();

            if (videos != null) return Ok(videos);

            return BadRequest(new { Message = "No videos" });

        }

        [HttpGet("GetUserVideos/{userId}")]
        public async Task<IActionResult> GetUserVideos(int userId)
        {

            var videos = await _rideTablesService.GetUserRideVideos(userId);

            if (videos != null) return Ok(videos);

            return BadRequest(new { Message = "This user has no videos" });

        }

        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromBody] RideVideosModel video)
        {
            var success = await _rideTablesService.AddRideVideo(video);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "Video creation was not successful" });
        }

        [HttpPost("EditVideo")]
        public async Task<IActionResult> EditVideo([FromBody] RideVideosModel video)
        {
            var success = await _rideTablesService.EditRideVideo(video);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "No video found" });
        }

        [HttpDelete("RemoveVideo")]
        public async Task<IActionResult> RemoveVideo([FromBody] RideVideosModel video)
        {
            var success = await _rideTablesService.EditRideVideo(video);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "Video Deletion was not successful" });
        }


        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLikes([FromBody] LikesModel like)
        {

            var success = await _rideTablesService.AddLike(like);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "The like was not given" });

        }

        [HttpDelete("RemoveGalleryLike/{userId}/{postId}")]
        public async Task<IActionResult> RemoveGalleryLike(int userId, int postId)
        {
            var success = await _rideTablesService.RemoveGalleryPostLike(userId, postId);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no like to be removed was found" });

        }

        [HttpDelete("RemoveRouteLike/{userId}/{postId}")]
        public async Task<IActionResult> RemoveRouteLike(int userId, int postId)
        {
            var success = await _rideTablesService.RemoveRouteLike(userId, postId);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no like to be removed was found" });

        }

        [HttpDelete("RemoveVideoLike/{userId}/{postId}")]
        public async Task<IActionResult> RemoveVideoLike(int userId, int postId)
        {
            var success = await _rideTablesService.RemoveVideoLike(userId, postId);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no like to be removed was found" });

        }

        [HttpDelete("RemoveCommentLike/{userId}/{postId}")]
        public async Task<IActionResult> RemoveCommentLike(int userId, int postId)
        {
            var success = await _rideTablesService.RemoveCommentLike(userId, postId);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no like to be removed was found" });

        }


        [HttpGet("GetUserLikes/{userId}")]
        public async Task<IActionResult> GetUserLikes(int userId)
        {

            var Likes = await _rideTablesService.GetLikesById(userId);

            if (Likes != null) return Ok(Likes);

            return BadRequest(new { Message = "This user has no routes" });

        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentsModel comment)
        {

            var success = await _rideTablesService.AddComment(comment);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "The comments was not submited" });

        }

        [HttpDelete("RemoveComment/{commentId}/{userId}")]
        public async Task<IActionResult> RemoveComment(int commentId, int userId)
        {
            var success = await _rideTablesService.RemoveComment(commentId, userId);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "no comment to be removed was found" });

        }

        [HttpGet("GetRouteComments/{routeId}")]
        public async Task<IActionResult> GetRouteComments(int routeId)
        {

            var comments = await _rideTablesService.GetCommentsByRouteId(routeId);

            if (comments != null) return Ok(comments);

            return BadRequest(new { Message = "No comments" });

        }

        [HttpGet("GetGalleryComments/{galleryId}")]
        public async Task<IActionResult> GetGalleryComments(int galleryId)
        {

            var comments = await _rideTablesService.GetCommentsByGalleryPostId(galleryId);

            if (comments != null) return Ok(comments);

            return BadRequest(new { Message = "No comments" });



        }

        [HttpGet("GetVideoComments/{videoId}")]
        public async Task<IActionResult> GetVideoComments(int videoId)
        {

            var comments = await _rideTablesService.GetCommentsByVideoId(videoId);

            if (comments != null) return Ok(comments);

            return BadRequest(new { Message = "No comments" });



        }

        [HttpPost("AddUserProfile")]
        public async Task<IActionResult> AddUserProfile([FromBody] UserProfileModel profile)
        {
            var success = await _rideTablesService.AddUserProfile(profile);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "Profile creation was not successful" });
        }

        [HttpPost("EditProfile")]
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileModel profile)
        {
            var success = await _rideTablesService.EditUserProfile(profile);

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Message = "No profile found" });
        }

        [HttpGet("GetProfile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _rideTablesService.GetProfileById(id);

            if (profile != null) return Ok(profile);

            return BadRequest(new { Message = "No profile found" });
        }


    }
}