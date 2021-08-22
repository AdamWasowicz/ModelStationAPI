using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModelStationAPI.Interfaces;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/comment")]
    public class CommentController : Controller
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CommentController(ModelStationDbContext dbContext, IMapper mapper, ICommentService commentService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommentDTO>> GetAll()
        {
            var commentsDTO = _commentService.GetAll();
            return Ok(commentsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<CommentDTO> GetById([FromRoute] int id)
        {
            var commentsDTO = _commentService.GetById(id);

            if (commentsDTO == null)
                return NotFound();

            return Ok(commentsDTO);      
        }

        [HttpGet("post/{id}")]
        public ActionResult<IEnumerable<CommentDTO>> GetCommentsByPostId([FromRoute] int id)
        {
            var commentsDTO = _commentService.GetCommentsByPostId(id);

            if (commentsDTO == null)
                return NotFound();

            return Ok(commentsDTO);
        }

        [HttpGet("user/{id}")]
        public ActionResult<IEnumerable<CommentDTO>> GetCommentsByUserId([FromRoute] int id)
        {
            var commentsDTO = _commentService.GetCommentsByUserId(id);

            if (commentsDTO == null)
                return NotFound();

            return Ok(commentsDTO);
        }

        [HttpPost]
        public ActionResult CreateComment([FromBody] CreateCommentDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdId = _commentService.Create(dto);

            return Created(createdId.ToString(), null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _commentService.Delete(id);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPatch]
        public ActionResult EditComment([FromBody] EditCommentDTO dto)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = _commentService.Edit(dto);

            if (result)
                return Ok();

            return NotFound();
        }
    }
}
