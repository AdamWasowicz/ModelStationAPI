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
    [Route("api/v1/likedpost")]
    [ApiController]
    public class LikedPostController : Controller
    {
        private readonly ILikedPostService _likedPostService;

        public LikedPostController(ILikedPostService likedPostService)
        {
            _likedPostService = likedPostService;
        }

        [HttpGet]
        public ActionResult<List<LikedPostDTO>> GetAll()
        {
            var likedPostsDTO = _likedPostService.GetAll();
            return Ok(likedPostsDTO);
        }

        [HttpGet("likedpost/id/{id}")]
        [Authorize(Policy = "IsUser")]
        public ActionResult<LikedPostDTO> GetById([FromRoute] int id)
        {
            var likedPostDTO = _likedPostService.GetById(id);

            if (likedPostDTO == null)
                return NotFound();

            return Ok(likedPostDTO);

        }

        [HttpGet("post/id/{id}")]
        [Authorize(Policy = "IsUser")]
        public ActionResult<LikedPostDTO> GetByPostId([FromRoute] int id)
        {
            var likedPostDTO = _likedPostService.GetByPostId(id, User);

            if (likedPostDTO == null)
                return NotFound();

            return Ok(likedPostDTO);

        }

        [HttpPost]
        [Authorize(Policy = "IsUser")]
        public ActionResult CreateLikedPost([FromBody] CreateLikedPostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdId = _likedPostService.Create(dto);

            return Created(createdId.ToString(), null);
        }

        [HttpPatch("likedpost/id")]
        [Authorize(Policy = "IsUser")]
        public ActionResult EditLikedPostByLikedPostId([FromBody] EditLikedPostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _likedPostService.EditByPostId(dto, User);

            if (result == true)
                return Ok();

            return NotFound();
        }

        [HttpPatch("post/id")]
        [Authorize(Policy = "IsUser")]
        public ActionResult EditLikedPostByPostId([FromBody] EditLikedPostDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _likedPostService.Edit(dto);

            if (result == true)
                return Ok();

            return NotFound();
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<LikedPostDTO>> GetLikedPostByUserId([FromRoute] int id)
        {
            var likedPosts = _likedPostService.GetLikedPostsByUserId(id);
            return likedPosts;
        }

        [HttpGet("post/{id}")]
        public ActionResult<List<UserDTO>> GetUsersByPostId([FromRoute] int id)
        {
            var users = _likedPostService.GetUsersByPostId(id);
            return users;
        }
    }
}
