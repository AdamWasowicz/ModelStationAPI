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
    public class PostService : IPostService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PostDTO GetById(int id)
        {
            var post = _dbContext
                .Posts
                .Include(u => u.User)
                .Include(pc => pc.PostCategory)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
                throw new NotFoundException("Post does not exist");

            var result = _mapper.Map<PostDTO>(post);
            return result;
        }

        public List<PostDTO> GetAll()
        {
            var posts = _dbContext
                .Posts
                .Include(u => u.User)
                .Include(pc => pc.PostCategory)
                .ToList();

            var result = _mapper.Map<List<PostDTO>>(posts);
            return result;
        }

        public int Create(CreatePostDTO dto)
        {
            var post = _mapper.Map<Post>(dto);

            post.IsActive = true;
            post.IsBanned = false;
            post.CreationDate = DateTime.Now;
            post.LastEditDate = post.CreationDate;
            post.Likes = 0;

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            return post.Id;
        }

        public bool Delete(int id)
        {
            var post = _dbContext
                .Posts
                .FirstOrDefault(p => p.Id == id);

            if (post is null)
                throw new NotFoundException("Post does not exist");

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return true;
        }

        public List<PostDTO> GetPostsByUserId(int userId)
        {
            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                .Where(p => p.UserId == userId)
                .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("Search yelded no result");

            var result = _mapper.Map<List<PostDTO>>(posts);
            return result;
        }

        public List<PostDTO> GetPostsByPostCategoryId(int categoryId)
        {
            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                .Where(p => p.PostCategoryId == categoryId)
                .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("Search yelded no result");

            var result = _mapper.Map<List<PostDTO>>(posts);
            return result;
        }

        public List<PostDTO> GetPostsByUserName(string userName)
        {
            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                .Where(p => p.User.UserName == userName)
                .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("Search yelded no result");

            var result = _mapper.Map<List<PostDTO>>(posts);
            return result;
        }

        public List<PostDTO> GetPostsByPostCategoryName(string categoryName)
        {
            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                .Where(p => p.PostCategory.Name == categoryName)
                .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("Search yelded no result");

            var result = _mapper.Map<List<PostDTO>>(posts);
            return result;
        }
    }
}
