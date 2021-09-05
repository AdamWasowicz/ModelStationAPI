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
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ModelStationAPI.Authorization;

namespace ModelStationAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ModelStationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IAuthorizationService _authorizationService;

        public PostService(ModelStationDbContext dbContext,
            IMapper mapper, 
            IFileService fileService,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _authorizationService = authorizationService;
        }


        //Complementary functions
        private void IncludeFiles(PostDTO dto)
        {
            dto.Files = _fileService.GetFilesByPostId(dto.Id);
        }
        private void IncludeComments(PostDTO dto)
        {
            int id = dto.Id;
            var comments = _dbContext
                .Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                    .Where(c => c.IsBanned == false)
                    .Where(c => c.IsActive == false)
                    .Where(c => c.PostId == id)
                        .ToList();

            var commentsDTO = _mapper.Map<List<CommentDTO>>(comments);
            dto.Comments = commentsDTO;
        }
        private Tuple<bool, Post> CheckIfPostExistAndReturnPostIfItExists(int postId)
        {
            var post = _dbContext
                .Posts
                    .Where(p => p.Id == postId)
                        .FirstOrDefault();

            if (post == null)
                return Tuple.Create(false, new Post());

            return Tuple.Create(false, post);
        }


        //Functions
        public int Create(CreatePostDTO dto, ClaimsPrincipal userClaims)
        {
            var post = _mapper.Map<Post>(dto);
            post.UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value); ;
            post.IsActive = true;
            post.IsBanned = false;
            post.CreationDate = DateTime.Now;
            post.LastEditDate = post.CreationDate;
            post.Likes = 0;

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.Create));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");

            //Post
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            //Files
            foreach (var file in dto.Files)
            {
                var createFileDto = new CreateFileStorageDTO()
                {
                    UserId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value),
                    PostId = post.Id,
                    ContentType = "POST",
                    File = file
                };

                _fileService.Upload(createFileDto);
            }

            return post.Id;
        }
        public bool Delete(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var post = _dbContext
                .Posts
                .Where(p => p.Id == id)
                    .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no Post with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.Delete));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");



            if (post.UserId != userId)
                throw new NoPermissionException("You can't delete someone else's post");



            //HERE DELETE FILES OF THAT POST
            var files = _dbContext
                .FilesStorage
                    .Where(fs => fs.ContentType == "POST")
                    .Where(fs => fs.PostId == id)
                        .ToList();

            foreach (var file in files)
            {
                if (!File.Exists(file.FullPath))
                    throw new FileNotFoundException();

                File.Delete(file.FullPath);
                if (File.Exists(file.FullPath))
                    throw new Exception("File could not be deleted");

                _dbContext.FilesStorage.Remove(file);
                _dbContext.SaveChanges();
            }

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return true;
        }
        public bool Edit(EditPostDTO dto, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var post = _dbContext
                .Posts
                .Where(p => p.Id == dto.Id)
                .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no Post with that Id");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.Update));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");



            if (post.UserId != userId)
                throw new NoPermissionException("You can't delete someone else's post");

            //Changes
            if (dto.Title != null && dto.Title.Length > 0)
                post.Title = dto.Title;

            if (dto.Text != null && dto.Text.Length > 0)
                post.Text = dto.Text;

            if (dto.PostCategoryId != null && dto.PostCategoryId != 0)
                post.PostCategoryId = dto.PostCategoryId;

            post.LastEditDate = DateTime.Now;

            _dbContext.SaveChanges();

            return true;
        }


        public bool UnBanPostByPostId(int postId, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var post = _dbContext
                .Posts
                    .Where(p => p.Id == postId)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no Post with that PostId");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.Ban));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");



            post.IsBanned = false;
            _dbContext.SaveChanges();

            return true;
        }
        public bool BanPostByPostId(int postId, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var post = _dbContext
                .Posts
                    .Where(p => p.Id == postId)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no Post with that PostId");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.Ban));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");



            post.IsBanned = true;
            _dbContext.SaveChanges();

            return true;
        }
        public bool BanPostsByUserId(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var posts = _dbContext
                .Posts
                    .Where(p => p.UserId == id)
                        .ToList();

            foreach (var post in posts)
            {
                var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                    new ResourceOperationRequirementPost(ResourceOperation.Ban));

                if (authorizationResult.IsCompletedSuccessfully)
                    post.IsBanned = true;
            }

            _dbContext.SaveChanges();

            return true;
        }
        public bool UnBanPostsByUserId(int id, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var user = _dbContext
                .Users
                    .Where(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var posts = _dbContext
                .Posts
                    .Where(p => p.UserId == id)
                        .ToList();

            foreach (var post in posts)
            {
                var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                    new ResourceOperationRequirementPost(ResourceOperation.Ban));

                if (authorizationResult.IsCompletedSuccessfully)
                    post.IsBanned = false;
            }

            _dbContext.SaveChanges();

            return true;
        }
        public bool ChangeActiveStateByPostId(int postId, ClaimsPrincipal userClaims)
        {
            int userId = Convert.ToInt32(userClaims.FindFirst(c => c.Type == "UserId").Value);

            var post = _dbContext
                .Posts
                    .Where(p => p.Id == postId)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no Post with that PostId");

            var authorizationResult = _authorizationService.AuthorizeAsync(userClaims, post,
                new ResourceOperationRequirementPost(ResourceOperation.ChangeActivity));

            if (!authorizationResult.IsCompletedSuccessfully)
                throw new NoPermissionException("This user do not have premission to do that");



            post.IsActive = !post.IsActive;
            _dbContext.SaveChanges();

            return true;
        }


        public List<PostDTO> GetAll()
        {
            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts in database");

            posts = posts
                .Where(p => p.IsActive == true)
                .Where(p => p.IsBanned == false)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts to be displayed");


            var postsDTO = _mapper.Map<List<PostDTO>>(posts);


            //Files
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeFiles(postsDTO[i]);

            //Comments
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeComments(postsDTO[i]);             

            return postsDTO;
        }
        public PostDTO GetById(int id)
        {
            var post = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                    .Where(p => p.Id == id)
                        .FirstOrDefault();

            if (post == null)
                throw new NotFoundException("There is no post with that Id");

            if (!(post.IsActive == true) && (post.IsBanned == false))
                throw new NotFoundException("This post is Banned or not Active");

            var postDTO = _mapper.Map<PostDTO>(post);


            //Files
            IncludeFiles(postDTO);

            //Comments
            IncludeComments(postDTO);

            return postDTO;
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
                throw new NotFoundException("There are no posts with that CategoryId");

            posts = posts
                .Where(p => p.IsActive == true)
                .Where(p => p.IsBanned == false)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts to be displayed");


            var postsDTO = _mapper.Map<List<PostDTO>>(posts);

            //Files
            for (int i = 0; i < posts.Count; i++)
                IncludeFiles(postsDTO[i]);

            //Comments
            for (int i = 0; i < posts.Count; i++)
                IncludeComments(postsDTO[i]);

            return postsDTO;
        }
        public List<PostDTO> GetPostsByPostCategoryName(string categoryName)
        {
            //Check if postCategpry exists
            var postCategoryExists = _dbContext
                .PostCategories
                    .Where(pc => pc.Name == categoryName)
                        .FirstOrDefault();

            if (postCategoryExists == null)
                throw new NotFoundException("There is no PostCategory with that Name");

            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                    .Where(p => p.Id == postCategoryExists.Id)
                        .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts with that PostCategory");

            posts = posts
                .Where(p => p.IsActive == true)
                .Where(p => p.IsBanned == false)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts to be displayed");


            var postsDTO = _mapper.Map<List<PostDTO>>(posts);


            //Files
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeFiles(postsDTO[i]);

            //Comments
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeComments(postsDTO[i]);

            return postsDTO;
        }
        public List<PostDTO> GetPostsByUserId(int userId)
        {
            var user = _dbContext
                .Users
                    .Where(u => u.Id == userId)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("There is no User with that Id");

            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                    .Where(p => p.UserId == userId)
                        .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts with that Owner's UserId");

            posts = posts
                .Where(p => p.IsActive == true)
                .Where(p => p.IsBanned == false)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts to be displayed");

            var postsDTO = _mapper.Map<List<PostDTO>>(posts);


            //Files
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeFiles(postsDTO[i]);

            //Comments
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeComments(postsDTO[i]);

            return postsDTO;
        }
        public List<PostDTO> GetPostsByUserName(string userName)
        {
            var user = _dbContext
                .Users
                    .Where(u => u.UserName == userName)
                        .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("There is no User with that UserName");

            var posts = _dbContext
                .Posts
                .Include(p => p.User)
                .Include(p => p.PostCategory)
                    .Where(p => p.User.UserName == userName)
                        .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts with that Owner's UserName");

            posts = posts
                .Where(p => p.IsActive == true)
                .Where(p => p.IsBanned == false)
                    .ToList();

            if (posts.Count == 0)
                throw new NotFoundException("There are no posts to be displayed");

            var postsDTO = _mapper.Map<List<PostDTO>>(posts);


            //Files
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeFiles(postsDTO[i]);

            //Comments
            for (int i = 0; i < postsDTO.Count; i++)
                IncludeComments(postsDTO[i]);

            return postsDTO;
        }
    }
}
