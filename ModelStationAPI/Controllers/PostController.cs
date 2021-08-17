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
        public ActionResult<List<PostDTO>> GetAll()
        {
            var postsDTO = _postService.GetAll();
            return Ok(postsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<PostDTO> GetById([FromRoute] int id)
        {
            var postDTO = _postService.GetById(id);

            if (postDTO == null)
                return NotFound();

            return Ok(postDTO);
        }

        [HttpPost]
        public ActionResult CreatePost([FromBody] CreatePostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdId = _postService.Create(dto);

            return Created(createdId.ToString(), null);
        }

        [HttpDelete]
        public ActionResult DeletePost([FromRoute] int id)
        {
            bool isDeleted = _postService.Delete(id);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<PostDTO>> GetPostByUserId(int id)
        {
            var postsDTO = _postService.GetPostsByUserId(id);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("postcategory/{id}")]
        public ActionResult<List<PostDTO>> GetPostByCategoryId(int id)
        {
            var postsDTO = _postService.GetPostsByPostCategoryId(id);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("user/{name}")]
        public ActionResult<List<PostDTO>> GetPostByUserName(string name)
        {
            var postsDTO = _postService.GetPostsByUserName(name);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }

        [HttpGet("postcategory/{name}")]
        public ActionResult<List<PostDTO>> GetPostByCategoryName(string name)
        {
            var postsDTO = _postService.GetPostsByPostCategoryName(name);

            if (postsDTO == null)
                return NotFound();

            return Ok(postsDTO);
        }
    }
}
