using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModelStationAPI.Entities;
using ModelStationAPI.Exceptions;
using ModelStationAPI.Interfaces;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostCategoryService(ModelStationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<PostCategoryDTO> GetAll()
        {
            var postCategories = _dbContext
                .PostCategories
                .ToList();

            var postCategoriesDTO = _mapper.Map<List<PostCategoryDTO>>(postCategories);
            return postCategoriesDTO;
        }

        public PostCategoryDTO GetById(int id)
        {
            var postCategory = _dbContext
                .PostCategories
                .Where(pc => pc.Id == id)
                .FirstOrDefault();

            if (postCategory == null)
                throw new NotFoundException("There is no post category with that Id");

            var postCategoryDTO = _mapper.Map<PostCategoryDTO>(postCategory);
            return postCategoryDTO;
        }

        public int Create(CreatePostCategoryDTO dto)
        {
            var postCategory = _mapper.Map<PostCategory>(dto);

            _dbContext.PostCategories.Add(postCategory);
            _dbContext.SaveChanges();

            return postCategory.Id;
        }

        public bool Delete(int id)
        {
            var postCategory = _dbContext
                .PostCategories
                .Where(pc => pc.Id == id)
                .FirstOrDefault();

            if (postCategory is null)
                throw new NotFoundException("There is no post category with that Id");

            _dbContext.PostCategories.Remove(postCategory);
            _dbContext.SaveChanges();

            //ClearMissingPostCategories
            ClearMissingPostCategories(id);

            return true;
        }

        private void ClearMissingPostCategories(int postCategoryId)
        {
            var posts = _dbContext
                .Posts
                .Where(p => p.PostCategoryId == postCategoryId)
                .ToList();

            foreach (var post in posts)
                post.PostCategoryId = null;

            _dbContext.SaveChanges();
        }
    }
}
