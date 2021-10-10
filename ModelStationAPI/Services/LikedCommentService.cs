using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ModelStationAPI.Services
{
    public class LikedCommentService : ILikedCommentService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LikedCommentService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LikedCommentDTO> GetAll()
        {
            var likedComments = _dbContext
                .LikedComments
                .ToList();

            var likedCommentsDTO = _mapper.Map<List<LikedCommentDTO>>(likedComments);
            return likedCommentsDTO;
        }

        public LikedCommentDTO GetById(int id)
        {
            var likedComment = _dbContext
                .LikedComments
                .Where(lc => lc.Id == id)
                .FirstOrDefault();

            if (likedComment == null)
                throw new NotFoundException("There is no LikedComment with that Id");

            var likedCommentDTO = _mapper.Map<LikedCommentDTO>(likedComment);
            return likedCommentDTO;
        }

        public int Create(CreateLikedCommentDTO dto)
        {
            var likedComment = _mapper.Map<LikedComment>(dto);

            likedComment.CreationDate = DateTime.Now;

            _dbContext.LikedComments.Add(likedComment);

            var comment = _dbContext
                .Comments
                    .Where(c => c.Id == dto.CommentId)
                        .FirstOrDefault();

            comment.Likes = comment.Likes + dto.Value;

            if (dto.Value == 0)
                _dbContext.LikedComments.Remove(likedComment);
            else
                likedComment.Value = dto.Value;

            _dbContext.SaveChanges();

            return likedComment.Id;
        }

        public bool Edit(EditLikedCommentDTO dto)
        {
            var likedComment = _dbContext
                .LikedComments
                    .Where(lc => lc.Id == dto.Id)
                    .FirstOrDefault();

            if (likedComment == null)
                throw new NotFoundException("There is not LikedComment with that Id");

            var comment = _dbContext
                .Comments
                .Where(c => c.Id == likedComment.CommentId)
                .FirstOrDefault();

            comment.Likes = comment.Likes - likedComment.Value + dto.Value;

            if (dto.Value == 0)
                _dbContext.LikedComments.Remove(likedComment);
            else
                likedComment.Value = dto.Value;

            _dbContext.SaveChanges();
            return true;
        }

        public List<LikedCommentDTO> GetLikedCommentsByUserId(int userId)
        {
            var likedComments = _dbContext
                .LikedComments
                .Where(lc => lc.UserId == userId)
                .ToList();

            if (likedComments.Count == 0)
                throw new NotFiniteNumberException("There is no LikedComment with that UserId");

            var likedCommentsDTO = _mapper.Map<List<LikedCommentDTO>>(likedComments);
            return likedCommentsDTO;
        }

        public List<UserDTO> GetUsersByCommentId(int commentId)
        {
            var likedComments = _dbContext
                .LikedComments
                .Include(lp => lp.User)
                .Where(lp => lp.CommentId == commentId)
                .ToList();

            if (likedComments.Count == 0)
                throw new NotFoundException("There is no LikedComment with that CommentId");

            var userList = new List<User>();
            foreach (var likedComment in likedComments)
                userList.Append(likedComment.User);

            var usersDTO = _mapper.Map<List<UserDTO>>(userList);
            return usersDTO;
        }
    }
}
