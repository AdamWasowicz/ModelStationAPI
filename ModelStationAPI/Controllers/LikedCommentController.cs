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
    [Route("api/v1/likedcomment")]
    [ApiController]
    public class LikedCommentController : Controller
    {
        private readonly ILikedCommentService _likedCommentService;

        public LikedCommentController(ILikedCommentService likedCommentService)
        {
            _likedCommentService = likedCommentService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] CreateLikedCommentDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdId = _likedCommentService.Create(dto);
            return Created(createdId.ToString(), null);
        }

        [HttpPatch]
        [Authorize]
        public ActionResult Edit([FromBody] EditLikedCommentDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _likedCommentService.Edit(dto);

            if (result)
                return Ok();

            return NotFound();
        }

        //To be added
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete([FromBody] int id)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public ActionResult<List<LikedCommentDTO>> GetAll()
        {
            var result = _likedCommentService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<LikedCommentDTO> GetById([FromRoute] int id)
        {
            var result = _likedCommentService.GetById(id);
            return Ok(result);
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<LikedCommentDTO>> GetLikedCommentsByUserId([FromRoute] int id)
        {
            var result = _likedCommentService.GetLikedCommentsByUserId(id);
            return Ok(result);
        }

        [HttpGet("comment/{id}")]
        public ActionResult<List<LikedCommentDTO>> GetUsersByCommentId([FromRoute] int id)
        {
            var result = _likedCommentService.GetUsersByCommentId(id);
            return Ok(result);
        }
    }
}
