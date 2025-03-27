using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TablesAPI.Services;

namespace TablesAPI.Controllers
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

            if(posts != null) return Ok(posts);

            return BadRequest(new {Message = "The gallery is empty.."});

        }

        [HttpGet("GetUserPosts/{userId}")]
        public async Task<IActionResult> GetUserPosts(int userId)
        {

            var posts = await _rideTablesService.GetUserPosts(userId);

            if(posts != null) return Ok(posts);

            return BadRequest(new {Message = "This user's gallery is empty.."});

        }
        
        [HttpPost("EditPost")]
        public async Task<IActionResult> EditGalleryPost([FromBody] GalleryPostModel post)
        {
            var success = await _rideTablesService.EditGalleryPost(post);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "No post found"});
        }

        [HttpPost("AddGalleryPost")]
        public async Task<IActionResult> AddGalleryPost([FromBody] GalleryPostModel post)
        {

            var success = await _rideTablesService.AddGalleryPost(post); 

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "Post creation was not successful"});

        }

        [HttpGet("GetRoutes")]
        public async Task<IActionResult> GetAllRoutes()
        {

            var routes = await _rideTablesService.GetRoutes();

            if(routes != null) return Ok(routes);

            return BadRequest(new {Message = "No routes"});

        }

        [HttpGet("GetUserRoutes/{userId}")]
        public async Task<IActionResult> GetUserRoutes(int userId)
        {

            var routes = await _rideTablesService.GetUserRoutes(userId);

            if(routes != null) return Ok(routes);

            return BadRequest(new {Message = "This user has no routes"});

        }

         [HttpPost("AddRoute")]
        public async Task<IActionResult> AddRoute([FromBody] RoutesModel route)
        {
            var success = await _rideTablesService.AddRoute(route); 

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "Route creation was not successful"});
        }

        [HttpPost("PrivateRoute/{id}")]
        public async Task<IActionResult> PrivateRoute(int id)
        {
             var success = await _rideTablesService.PrivateRoute(id); 

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "No route found"});
        }

        [HttpDelete("RemoveRoute/{id}")]
        public async Task<IActionResult> RemoveLike(int id)
        {
            var success = await _rideTablesService.RemoveRoute(id);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "no route to be removed was found"});

        }

        [HttpGet("GetLikes/{id}")]
        public async Task<IActionResult> GetLikes(int id)
        {
            var likes = await _rideTablesService.GetLikes(id);

            if(likes != null) return Ok(likes);

            return BadRequest(new {Message = "No likes"});
        }

         [HttpGet("GetComments/{id}")]
        public async Task<IActionResult> GetComments(int id)
        {
            var Comments = await _rideTablesService.GetComments(id);

            if(Comments != null) return Ok(Comments);

            return BadRequest(new {Message = "No comments"});
        }
        
        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLikes([FromBody] LikesModel like)
        {

            var success = await _rideTablesService.AddLike(like);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "The like was not given"});

        }

        [HttpDelete("RemoveLike/{userId}/{postId}")]
        public async Task<IActionResult> RemoveLike(int userId, int postId)
        {
            var success = await _rideTablesService.RemoveLike(userId, postId);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "no like to be removed was found"});

        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentsModel comment)
        {

            var success = await _rideTablesService.AddComment(comment);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "The comments was not submited"});

        }

          [HttpDelete("RemoveComment/{commentId}/{userId}")]
        public async Task<IActionResult> RemoveComment(int commentId, int userId)
        {
            var success = await _rideTablesService.RemoveComment(commentId, userId);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Message = "no comment to be removed was found"});

        }



    }
}