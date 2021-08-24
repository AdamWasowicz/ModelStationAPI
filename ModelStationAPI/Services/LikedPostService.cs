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
    public class LikedPostService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LikedPostService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LikedPost> GetAll()
        {
            var likedPosts = _dbContext
                .LikedPosts
                .ToList();

            var likedPostsDTO = _mapper.Map<List<LikedPost>>(likedPosts);
            return likedPostsDTO;
        }

        //GetById
        //HERE

        public int Create(CreateLikedPostDTO dto)
        {
            var likedPost = _mapper.Map<LikedPost>(dto);

            likedPost.CreationDate = DateTime.Now;

            _dbContext.LikedPosts.Add(likedPost);
            _dbContext.SaveChanges();

            return likedPost.Id;
        }
    }
}
