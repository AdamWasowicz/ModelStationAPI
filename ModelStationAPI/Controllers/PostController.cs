using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using ModelStationAPI.Services;
using ModelStationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/post")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        //[Authorize(Policy = "IsAdmin")]
        public ActionResult<List<PostDTO>> GetAll()
        {
            var postsDTO = _postService.GetAll();
            return Ok(postsDTO);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<PostDTO> GetById([FromRoute] int id)
        {
            var postDTO = _postService.GetById(id);

            if (postDTO == null)
                return NotFound();

            return Ok(postDTO);
        }

        [HttpGet("query")]
        [AllowAnonymous]
        public ActionResult<PostQuerryResult> GetByQuery([FromQuery] PostQuery query)
        {
            var result = _postService.GetByQuery(query);

            if (result.Posts.Count == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("banner/title/{title}")]
        [AllowAnonymous]
        public ActionResult<List<PostBannerDTO>> GetBannersByTitle([FromRoute] string title)
        {
            var results = _postService.SearchPostByTitle_ReturnBanners(title);

            if (results.Count == 0)
                return NoContent();

            return Ok(results);
        }

        [HttpGet("banner/query")]
        [AllowAnonymous]
        public ActionResult<List<PostBannerDTO>> GetBannersByQuery([FromQuery] PostQuery query)
        {
            var results = _postService.SearchByQuery_ReturnBanners(query);

            if (results.Count == 0)
                return NoContent();

            return Ok(results);
        }

        [HttpPost]
        [Authorize(Policy = "IsUser")]
        public ActionResult CreatePost([FromForm] CreatePostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == "UserId").Value);
            int createdId = _postService.Create(dto, User);

            return Created(createdId.ToString(), null);
        }

        [HttpPost("withpostcategoryname")]
        [Authorize(Policy = "IsUser")]
        public ActionResult CreatePostWithPostCategoryName([FromForm] CreatePostWithPostCategoryNameDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == "UserId").Value);
            int createdId = _postService.CreateWithPostCategoryName(dto, User);

            return Created(createdId.ToString(), null);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsUser")]
        public ActionResult DeletePost([FromRoute] int id)
        {
            bool isDeleted = _postService.Delete(id, User);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet("user/id/{id}")]
        [AllowAnonymous]
        public ActionResult<List<PostDTO>> GetPostByUserId([FromRoute] int id)
        {
            var postsDTO = _postService.GetPostsByUserId(id);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("postcategory/{id}")]
        [AllowAnonymous]
        public ActionResult<List<PostDTO>> GetPostByCategoryId([FromRoute] int id)
        {
            var postsDTO = _postService.GetPostsByPostCategoryId(id);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("user/username/{username}")]
        [AllowAnonymous]
        public ActionResult<List<PostDTO>> GetPostByUserName([FromRoute] string username)
        {
            var postsDTO = _postService.GetPostsByUserName(username);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("postcategory/{name}")]
        [AllowAnonymous]
        public ActionResult<List<PostDTO>> GetPostByCategoryName([FromRoute] string name)
        {
            var postsDTO = _postService.GetPostsByPostCategoryName(name);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpPatch]
        [Authorize(Policy = "IsUser")]
        public ActionResult Edit([FromBody] EditPostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == "UserId").Value);
            var result = _postService.Edit(dto, User);

            if (result)
                return Ok();

            return NotFound();
        }



        //Moderation
        //PUT
        [HttpPut("unban/postid/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult UnBanPostByPostId([FromRoute] int id)
        {
            var result = _postService.UnBanPostByPostId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("ban/postid/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult BanPostByPostId([FromRoute] int id)
        {
            var result = _postService.BanPostByPostId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("ban/userid/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult BanPostByUserId([FromRoute] int id)
        {
            var result = _postService.BanPostsByUserId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("unban/userid/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult UnBanPostByUserId([FromRoute] int id)
        {
            var result = _postService.UnBanPostsByUserId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("changeactivity/userid/{id}")]
        [Authorize(Policy = "IsModerator")]
        public ActionResult ChangeActiveStateByPostId([FromRoute] int id)
        {
            var result = _postService.ChangeActiveStateByPostId(id, User);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
