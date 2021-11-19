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
using System.Security.Claims;

namespace ModelStationAPI.Services
{
    public class LikedPostService : ILikedPostService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LikedPostService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LikedPostDTO> GetAll()
        {
            var likedPosts = _dbContext
                .LikedPosts
                .ToList();

            var likedPostsDTO = _mapper.Map<List<LikedPostDTO>>(likedPosts);
            return likedPostsDTO;
        }

        public LikedPostDTO GetById(int id)
        {
            var likedPost = _dbContext
                .LikedPosts
                .Where(lp => lp.Id == id)
                .FirstOrDefault();

            if (likedPost == null)
                throw new NotFoundException("There is no LikedPost with that Id");

            var likedPostDTO = _mapper.Map<LikedPostDTO>(likedPost);
            return likedPostDTO;
        }

        public LikedPostDTO GetByPostId(int id, ClaimsPrincipal userClaims)
        {
            int UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);
            var likedPost = _dbContext
                .LikedPosts
                    .Where(lp => lp.PostId == id)
                    .Where(lp => lp.UserId == UserId)
                        .FirstOrDefault();

            if (likedPost == null)
                throw new NotFoundException("There is no LikedPost with that PostId");

            var likedPostDTO = _mapper.Map<LikedPostDTO>(likedPost);
            return likedPostDTO;
        }

        public int Create(CreateLikedPostDTO dto)
        {
            var likedPost = _mapper.Map<LikedPost>(dto);

            likedPost.CreationDate = DateTime.Now;

            _dbContext.LikedPosts.Add(likedPost);

            var post = _dbContext
                    .Posts
                        .Where(p => p.Id == likedPost.PostId)
                            .FirstOrDefault();

            post.Likes = post.Likes + dto.Value;

            if (dto.Value == 0)
                _dbContext.LikedPosts.Remove(likedPost);
            else
                likedPost.Value = dto.Value;

            _dbContext.SaveChanges();

            return likedPost.Id;
        }

        public bool EditByPostId(EditLikedPostDTO dto, ClaimsPrincipal userClaims)
        {
            int UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);
            var likedPost = _dbContext
                .LikedPosts
                    .Where(lp => lp.PostId == dto.Id)
                    .Where(lp => lp.UserId == UserId)
                        .FirstOrDefault();

            if (likedPost == null)
                return false;

            var post = _dbContext
                    .Posts
                        .Where(p => p.Id == likedPost.PostId)
                            .FirstOrDefault();

            //Reset and change value
            //100<+> -> <=>
            //99<=>
            //99<=> -> <->
            //98<->

            //99<=> -> <+>
            //100<+>

            //99<=> -> <->
            //98<->
            post.Likes = post.Likes - likedPost.Value + dto.Value;

            if (dto.Value == 0)
                _dbContext.LikedPosts.Remove(likedPost);
            else
                likedPost.Value = dto.Value;


            _dbContext.SaveChanges();

            return true;
        }

        public bool Edit(EditLikedPostDTO dto)
        {
            var likedPost = _dbContext
                .LikedPosts
                .Where(lp => lp.Id == dto.Id)
                .FirstOrDefault();

            if (likedPost == null)
                throw new NotFoundException("There is no LikedPost with that Id");

            var post = _dbContext
                    .Posts
                        .Where(p => p.Id == likedPost.PostId)
                            .FirstOrDefault();

            //Reset and change value
            //100<+> -> <=>
            //99<=>
            //99<=> -> <->
            //98<->

            //99<=> -> <+>
            //100<+>

            //99<=> -> <->
            //98<->
            post.Likes = post.Likes - likedPost.Value + dto.Value;

            if (dto.Value == 0)
                _dbContext.LikedPosts.Remove(likedPost);
            else
                likedPost.Value = dto.Value;


            _dbContext.SaveChanges();

            return true;
        }

        public bool CreateOrEdit(CreateOrEditLikedPostDTO dto, ClaimsPrincipal userClaims)
        {
            int UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var likedPost = _dbContext
                .LikedPosts
                    .Where(lp => lp.PostId == dto.PostId)
                    .Where(lp => lp.UserId == UserId)
                        .FirstOrDefault();


            if (likedPost == null)
            {
                //CREATE
                var newLikedPost = _mapper.Map<LikedPost>(likedPost);

                newLikedPost.UserId = UserId;
                newLikedPost.CreationDate = DateTime.Now;

                _dbContext.LikedPosts.Add(newLikedPost);

                var post = _dbContext
                        .Posts
                            .Where(p => p.Id == newLikedPost.PostId)
                                .FirstOrDefault();

                post.Likes = post.Likes + dto.Value;

                if (dto.Value == 0)
                    _dbContext.LikedPosts.Remove(newLikedPost);
                else
                    newLikedPost.Value = dto.Value;

                _dbContext.SaveChanges();

                return true;
            }
            else
            {
                //EDIT
                var post = _dbContext
                    .Posts
                        .Where(p => p.Id == likedPost.PostId)
                            .FirstOrDefault();

                post.Likes = post.Likes - likedPost.Value + dto.Value;

                if (dto.Value == 0)
                    _dbContext.LikedPosts.Remove(likedPost);
                else
                    likedPost.Value = dto.Value;


                _dbContext.SaveChanges();

                return true;
            }
        }

        public List<LikedPostDTO> GetLikedPostsByUserId(int userId)
        {
            var likedPosts = _dbContext
                .LikedPosts
                .Where(lp => lp.UserId == userId)
                .ToList();

            if (likedPosts.Count == 0)
                throw new NotFoundException("There is no LikedPost with that UserId");

            var likedPostsDTO = _mapper.Map<List<LikedPostDTO>>(likedPosts);
            return likedPostsDTO;
        }

        //To be considered
        public List<UserDTO> GetUsersByPostId(int postId)
        {
            var likedPosts = _dbContext
                .LikedPosts
                .Include(lp => lp.User)
                .Where(lp => lp.PostId == postId)
                .ToList();

            if (likedPosts.Count == 0)
                throw new NotFoundException("There is no LikedPost with that PostId");

            var userList = new List<User>();
            foreach (var likedPost in likedPosts)
                userList.Append(likedPost.User);

            var usersDTO = _mapper.Map<List<UserDTO>>(userList);
            return usersDTO;
        }
    }
}
