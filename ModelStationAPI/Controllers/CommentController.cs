using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ModelStationAPI.Controllers
{
    [Route("api/v1/comment")]
    public class CommentController : Controller
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentController(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommentDTO>> GetAll()
        {
            var comments = _dbContext
                .Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .ToList();

            var commentsDTO = _mapper.Map<List<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<CommentDTO> GetById([FromRoute] int id)
        {
            var comment = _dbContext
                .Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefault();

            if (comment == null)
                return NotFound();

            var commentDTO = _mapper.Map<CommentDTO>(comment);
            return Ok(commentDTO);      
        }

    }
}
